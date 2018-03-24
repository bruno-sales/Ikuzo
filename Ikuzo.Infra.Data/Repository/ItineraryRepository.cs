using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class ItineraryRepository : BaseRepository<Itinerary>, IItineraryRepository
    {
        public ItineraryRepository(Context.Context context) : base(context)
        {
        }
        
        public void RemoveFromLine(string lineId)
        {
            var itens = DbSet.Where(i => string.Equals(i.LineId.ToLower(), lineId.ToLower())).ToList();

            foreach (var obj in itens)
            {
                DbSet.Remove(obj);
            }
        }
    }
}
