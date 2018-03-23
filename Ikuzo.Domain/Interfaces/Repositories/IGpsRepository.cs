using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IGpsRepository : IBaseRepository<Gps>
    {
        Gps GetBusGps(string externalBusId);
        void RemoveFromLine(string externalLineId);
    }
}
