using AngularApp1.Server.Models;

namespace AngularApp1.Server.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
