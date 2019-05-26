using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Interfaces
{
    public interface ILineRepository : IBaseRepository<Line>
    {
        void LineBulkInsert(IEnumerable<Line> lines);
        Line Details(string lineId);
        Line Get(string lineId);
    }
}
