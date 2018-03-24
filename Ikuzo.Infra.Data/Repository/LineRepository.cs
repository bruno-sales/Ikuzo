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

        public Line Details(string lineId)
        {
            return DbSet
                .Include(i => i.Buses)
                .Include(i=>i.Itineraries)
                .FirstOrDefault(i => string.Equals(i.LineId.ToLower(), lineId.ToLower()));
        }
    }
}
