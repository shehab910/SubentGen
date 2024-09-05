using AngularApp1.Server.Models;

namespace AngularApp1.Server.Dtos
{
    public class IpAddressDto
    {
        public string IpAddressString { get; set; }
        public int SubnetId { get; set; }

        public IpAddressDto(IpAddress ipAddress)
        {
            IpAddressString = ipAddress.IpAddressString;
            SubnetId = ipAddress.SubnetId;
        }
    }
}
