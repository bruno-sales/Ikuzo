using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IGpsRepository : IBaseRepository<Gps>
    {
        Gps GetBusGps(string busId);
        void RemoveFromLine(string lineId);
        void RemoveAll();
        IEnumerable<Gps> GetNerbyBusesGps(decimal latitude, decimal longitude, decimal variance);
        IEnumerable<Gps> GetNerbyBusesGpsFromLine(string lineId);
        void GpsBulkInsert(IEnumerable<Gps> gpses);
    }
}
