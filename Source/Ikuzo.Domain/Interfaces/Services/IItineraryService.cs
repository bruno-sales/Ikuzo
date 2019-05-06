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
    }
}
