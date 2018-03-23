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

        public Gps GetBusGps(string externalBusId)
        {
            var gps = DbSet.FirstOrDefault(i => string.Equals(i.BusExternalId.ToLower(), externalBusId.ToLower()));

            return gps;
        }

        public void RemoveFromLine(string externalLineId)
        {
            var itens = DbSet.Where(i => string.Equals(i.LineExternalId.ToLower(), externalLineId.ToLower())).ToList();

            foreach (var obj in itens)
            {
                DbSet.Remove(obj);
            }
        }
    }
}
