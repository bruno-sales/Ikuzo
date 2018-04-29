using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IItineraryRepository : IBaseRepository<Itinerary>
    {
        IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal variance);
        void RemoveFromLine(string lineId);
        void ItineraryBulkInsert(IEnumerable<Itinerary> itineraries);
    }
}
