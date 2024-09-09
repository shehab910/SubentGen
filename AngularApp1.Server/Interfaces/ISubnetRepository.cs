using AngularApp1.Server.Models;

namespace AngularApp1.Server.Interfaces
{
    public interface ISubnetRepository
    {
        Task<bool> CreateSubnet(string ownerUserName, string subnetString);
        Task<Subnet?> GetSubnet(string subnetString, bool withOwners, bool withIps);
        Task<List<string>?> GetSubnetIps(string subnetString);
        Task<List<Subnet>> GetUserSubnets(string ownerUserName);

    }
}
