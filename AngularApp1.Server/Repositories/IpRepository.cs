using AngularApp1.Server.Data;
using AngularApp1.Server.Interfaces;
using AngularApp1.Server.Models;

namespace AngularApp1.Server.Repositories
{
    public class IpRepository : IIpRepository
    {
        private readonly AppDbContext _appContext;

        public IpRepository(AppDbContext appContext) { _appContext = appContext; }

        public List<IpAddress> IpAddresses()
        {
            throw new NotImplementedException();
        }
    }
}
