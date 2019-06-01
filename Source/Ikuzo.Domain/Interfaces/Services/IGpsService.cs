using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IGpsService
    {
        Gps GetModalGps(string externalModalId);
        void CreateGpses(IEnumerable<Gps> gpses);
        ValidationResult RemoveGpsesFromLine(string lineId); 
        ValidationResult RemoveAllGpses();
        IEnumerable<Gps> GetNerbyModalsGps(decimal latitude, decimal longitude, decimal distance, string line);
    }
}
