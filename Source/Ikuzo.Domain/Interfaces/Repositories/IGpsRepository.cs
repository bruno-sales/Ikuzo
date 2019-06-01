using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IGpsRepository : IBaseRepository<Gps>
    {
        Gps GetModalGps(string modalId);
        void RemoveFromLine(string lineId);
        void RemoveAll();
        IEnumerable<Gps> GetNerbyModalsGps(decimal latitude, decimal longitude, decimal distance);
        IEnumerable<Gps> GetNerbyModalsGpsFromLine(string lineId);
        void GpsBulkInsert(IEnumerable<Gps> gpses);
    }
}
