using System.Data.Entity;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class LineRepository : BaseRepository<Line>, ILineRepository
    {
        public LineRepository(Context.Context context) : base(context)
        {
        }

        public Line Details(int lineId)
        {
            return DbSet
                .Include(i => i.Buses).FirstOrDefault(i => i.LineId == lineId);
        }

        public Line Details(string externalLineId)
        {
            return DbSet
                .Include(i => i.Buses).FirstOrDefault(i => string.Equals(i.ExternalId.ToLower(), externalLineId.ToLower()));
        }
    }
}
