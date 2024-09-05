using AngularApp1.Server.Models;

namespace AngularApp1.Server.Interfaces
{
    public interface ISubnetRepository
    {
        List<Subnet> GetSubnets();
        Task<bool> CreateSubnet(string ownerUserName, string subnetString);
    }
}
