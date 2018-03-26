using System.Collections.Generic;
using System.Data.Entity;
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

        public IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal variance)
        {
            //Negatives Lat/Lon
            var startLatitude = latitude - variance;
            var endLatitude = latitude + variance;

            var startLongitude = longitude - variance;
            var endLongitude = longitude + variance;

            var itens = DbSet.Where(i => (i.Latitude >= startLatitude && i.Latitude <= endLatitude)
                                         && (i.Longitude >= startLongitude && i.Longitude <= endLongitude))
                        .Include(i => i.Line).ToList();

            var lines = itens.Select(i => i.Line).Distinct();

            return lines;

        }
    }
}
