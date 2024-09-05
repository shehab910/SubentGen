using AngularApp1.Server.Models;

namespace AngularApp1.Server.Dtos.Account
{
    public class OwnerDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }

        public OwnerDto(AppUser user)
        {
            Email = user.Email;
            UserName = user.UserName;
        }
    }
}
