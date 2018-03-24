using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IGpsRepository : IBaseRepository<Gps>
    {
        Gps GetBusGps(string busId);
        void RemoveFromLine(string lineId);
        void RemoveFromBus(string busId);
    }
}
