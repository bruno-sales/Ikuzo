using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IItineraryService
    { 
        void CreateItineraries(IEnumerable<Itinerary> itineraries);
        ValidationResult RemoveItinerariesFromLine(string lineId);
        ValidationResult RemoveAllItineraries();
        IEnumerable<Line> GetLocalToDestinyLines(decimal latitude1, decimal longitude1, decimal latitude2,
            decimal longitude2, List<int> tags, decimal distance);
        IEnumerable<Itinerary> GetLineItineraries(string lineId);
    }
}
