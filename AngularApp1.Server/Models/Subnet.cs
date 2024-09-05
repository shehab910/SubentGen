using System.Text.Json.Serialization;

namespace AngularApp1.Server.Models
{
    public class Subnet
    {
        public int Id { get; set; }
        public string SubnetCIDR { get; set; }
        public string FirstIpAddress { get; set; }
        public List<IpAddress> IpAddresses { get; set; }

        [JsonIgnore]
        public List<AppUser> Owners { get; set; }

        public Subnet()
        {
            Owners = new List<AppUser>();
        }
    }
}
