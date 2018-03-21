using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IGpsRepository : IBaseRepository<Gps>
    {
        void RemoveFromLine(string externalLineId);
    }
}
