using System;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface ILineRepository : IBaseRepository<Line>
    {
        Line Details(Guid lineId);
        Line Details(string externalLineId);
    }
}
