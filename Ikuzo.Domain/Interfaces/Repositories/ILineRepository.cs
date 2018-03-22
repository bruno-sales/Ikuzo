using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface ILineRepository : IBaseRepository<Line>
    {
        Line Details(int lineId);
        Line Details(string externalLineId);
    }
}
