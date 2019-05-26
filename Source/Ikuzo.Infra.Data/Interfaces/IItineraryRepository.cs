using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Interfaces
{
    public interface IItineraryRepository : IBaseRepository<Itinerary>
    {
        IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal variance);
        void RemoveFromLine(string lineId);
        void RemoveAll();
        void ItineraryBulkInsert(IEnumerable<Itinerary> itineraries);
    }
}
