using AngularApp1.Server.Models;

namespace AngularApp1.Server.Interfaces
{
    public interface IIpRepository
    {
        List<IpAddress> IpAddresses();
    }
}
