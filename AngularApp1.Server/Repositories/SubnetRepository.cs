using AngularApp1.Server.Controllers;
using AngularApp1.Server.Data;
using AngularApp1.Server.Interfaces;
using AngularApp1.Server.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Repositories
{
    public class SubnetRepository : ISubnetRepository
    {
        private readonly AppDbContext _appContext;

        public SubnetRepository(AppDbContext appContext) { _appContext = appContext; }

        public async Task<bool> CreateSubnet(string ownerUserName, string subnetAddress)
        {
            // find user by username
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.UserName == ownerUserName);
            if (user == null)
            {
                return false;
            }
            var subnetAndCidr = subnetAddress.Split('/');

            var existingSubnet = await _appContext.Subnets.FirstOrDefaultAsync(s => s.FirstIpAddress == subnetAndCidr[0] && s.SubnetCIDR == subnetAndCidr[1]);
            if (existingSubnet != null)
            {
                //FIXME: Throws error when saving
                // check if the user already an owner
                return true;
                existingSubnet.Owners.Add(user);

                await _appContext.SaveChangesAsync();
                return true;
            }
            Subnet subnet = new Subnet
            {
                FirstIpAddress = subnetAddress.Split('/')[0],
                SubnetCIDR = subnetAddress.Split('/')[1],
                Owners = new List<AppUser> { user }
            };

            var savedSubnet = await _appContext.AddAsync(subnet);
            await _appContext.SaveChangesAsync();
            var subnetId = savedSubnet.Entity.Id;

            List<string> stringIps = await SubnetController.GenerateIPsAsync(subnetAddress);

            List<IpAddress> ips = stringIps.Select(stringip => new IpAddress
            {
                IpAddressString = stringip,
                SubnetId = subnetId
            }).ToList();

            try
            {
                await _appContext.BulkInsertAsync(ips);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return true;
        }

        public async Task<List<Subnet>> GetSubnets(bool withOwners = false, bool withIps = false)
        {
            IQueryable<Subnet> subnetsBuilder = _appContext.Subnets;

            if (withOwners)
            {
                subnetsBuilder = subnetsBuilder.Include(s => s.Owners);
            }

            if (withIps)
            {
                subnetsBuilder = subnetsBuilder.Include(s => s.IpAddresses);
            }

            return await subnetsBuilder.ToListAsync();
        }

        public async Task<Subnet?> GetSubnet(string subnetString, bool withOwners = false, bool withIps = false)
        {
            var subnetVals = subnetString.Split("/");
            if (subnetVals.Length != 2 || !int.TryParse(subnetVals[1], out _) || !System.Net.IPAddress.TryParse(subnetVals[0], out _))
            {
                return null;
            }
            IQueryable<Subnet> subnetBuilder = _appContext.Subnets;

            if (withOwners)
            {
                subnetBuilder = subnetBuilder.Include(s => s.Owners);
            }

            if (withIps)
            {
                subnetBuilder = subnetBuilder.Include(s => s.IpAddresses);
            }

            return await subnetBuilder.FirstOrDefaultAsync(s => s.SubnetCIDR == subnetVals[1] && s.FirstIpAddress == subnetVals[0]);
        }

        public async Task<List<string>?> GetSubnetIps(string subnetString)
        {
            var subnetVals = subnetString.Split("/");
            if (subnetVals.Length != 2 || !int.TryParse(subnetVals[1], out _) || !System.Net.IPAddress.TryParse(subnetVals[0], out _))
            {
                return null;
            }
            var subnet = await _appContext.Subnets.Where(s => s.SubnetCIDR == subnetVals[1] && s.FirstIpAddress == subnetVals[0]).FirstOrDefaultAsync();
            if (subnet == null)
            {
                return null;
            }
            var ips = await _appContext.IpAddresses.Where(ip => ip.SubnetId == subnet.Id).Select(ip => ip.IpAddressString).ToListAsync();
            return ips;
        }
    }
}
