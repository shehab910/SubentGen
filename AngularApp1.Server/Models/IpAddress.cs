
namespace AngularApp1.Server.Models
{
    public class IpAddress
    {
        public int Id { get; set; }
        public string IpAddressString { get; set; }
        public int SubnetId { get; set; }
        public Subnet Subnet { get; set; } = null!;
    }
}
