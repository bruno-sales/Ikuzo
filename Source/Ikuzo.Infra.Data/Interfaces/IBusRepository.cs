using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Interfaces
{
    public interface IBusRepository : IBaseRepository<Bus>
    {
        Bus Details(string busId);
        void RemoveFromLine(string lineId);
        void BusBulkInsert(IEnumerable<Bus> buses);
    }
}
