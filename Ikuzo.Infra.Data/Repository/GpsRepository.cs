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

        public void RemoveFromLine(string externalLineId)
        {
            var itens = DbSet.Where(i => i.LineExternalId == externalLineId).ToList();

            foreach (var obj in itens)
            {
                DbSet.Remove(obj);
            }
        }
    }
}
