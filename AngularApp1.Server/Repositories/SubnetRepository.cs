using AngularApp1.Server.Controllers;
using AngularApp1.Server.Data;
using AngularApp1.Server.Interfaces;
using AngularApp1.Server.Models;
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
            if (user == null) {
                return false;
            }
            var subnetAndCidr = subnetAddress.Split('/');
            
            var existingSubnet = await _appContext.Subnets.FirstOrDefaultAsync(s => s.FirstIpAddress == subnetAndCidr[0] && s.SubnetCIDR == subnetAndCidr[1]);
            if (existingSubnet != null)
            {
                existingSubnet.Owners.Add(user);
                await _appContext.SaveChangesAsync();
                return true;
            }

            List<string> stringIps = await SubnetController.GenerateIPsAsync(subnetAddress);
            List<IpAddress> subnets = new List<IpAddress>();

            foreach (string ip in stringIps)
            {
                IpAddress ipAddress = new IpAddress
                {
                    IpAddressString = ip,
                };
                subnets.Add(ipAddress);
            }

            Subnet subnet = new Subnet
            {
                IpAddresses = subnets,
                FirstIpAddress = subnetAddress.Split('/')[0],
                SubnetCIDR = subnetAddress.Split('/')[1],
                Owners = new List<AppUser> { user }
            };


            await _appContext.Subnets.AddAsync(subnet);
            await _appContext.SaveChangesAsync();

            return true;
        }

        public List<Subnet> GetSubnets()
        {
            return _appContext.Subnets.ToList();
        }
    }
}
