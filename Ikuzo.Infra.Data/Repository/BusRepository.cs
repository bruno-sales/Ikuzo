using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(Context.Context context) : base(context)
        {
        }

        public override IEnumerable<Bus> GetAll()
        {
            return DbSet
                .Include(i => i.Line);
        }

        public Bus Details(int busId)
        {
            return DbSet
                .Include(i => i.Gps)
                .Include(i => i.Line)
                .FirstOrDefault(i => i.BusId == busId);
        }

        public Bus Details(string externalBusId)
        {
            return DbSet
                .Include(i => i.Gps)
                .Include(i => i.Line)
                .FirstOrDefault(i => string.Equals(i.ExternalId.ToLower(), externalBusId.ToLower()));
        }
    }
}
