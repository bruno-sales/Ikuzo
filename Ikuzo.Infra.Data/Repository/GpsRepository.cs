using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class GpsRepository : BaseRepository<Gps>, IGpsRepository
    {
        public GpsRepository(Context.Context context) : base(context)
        {
        }

        public Gps GetBusGps(string busId)
        {
            var gps = DbSet.FirstOrDefault(i => string.Equals(i.BusId.ToLower(), busId.ToLower()));

            return gps;
        }

        public void RemoveFromLine(string lineId)
        {
            var itens = DbSet.Where(i => string.Equals(i.LineId.ToLower(), lineId.ToLower())).ToList();

            foreach (var obj in itens)
            {
                DbSet.Remove(obj);
            }
        }

        public void RemoveFromBus(string busId)
        {
            var itens = DbSet.Where(i => string.Equals(i.LineId.ToLower(), busId.ToLower())).ToList();

            foreach (var obj in itens)
            {
                DbSet.Remove(obj);
            }
        }
    }
}
