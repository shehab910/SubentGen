using AngularApp1.Server.Dtos.Account;
using AngularApp1.Server.Models;

namespace AngularApp1.Server.Dtos
{
    public class SubnetDto
    {
        public string SubnetCIDR { get; }
        public string FirstIpAddress { get; }
        public List<IpAddressDto> IpAddresses { get; }
        public List<OwnerDto> Owners { get; }

        public SubnetDto
            (Subnet subnet)
        {
            SubnetCIDR = subnet.SubnetCIDR;
            FirstIpAddress = subnet.FirstIpAddress;
            IpAddresses = subnet.IpAddresses != null ?
                subnet.IpAddresses.Select(ip => new IpAddressDto(ip)).ToList()
                : null!;
            Owners = subnet.Owners != null ?
                subnet.Owners.Select(owner => new OwnerDto(owner)).ToList()
                : null!;
        }

    }
}
