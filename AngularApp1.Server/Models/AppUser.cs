using Microsoft.AspNetCore.Identity;

namespace AngularApp1.Server.Models
{
    public class AppUser: IdentityUser
    {
        public List<Subnet> Subnets { get; set; }
    }
}
