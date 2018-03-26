using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IItineraryService
    { 
        IEnumerable<Itinerary> CreateItineraries(IEnumerable<Itinerary> itineraries);
        ValidationResult RemoveItinerariesFromLine(string lineId);
    }
}
