using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Service.Interfaces
{
    public interface IGpsService
    {
        Gps GetBusGps(string externalBusId);
        void CreateGpses(IEnumerable<Gps> gpses);
        ValidationResult RemoveGpsesFromLine(string lineId);
        ValidationResult RemoveGpsesFromBus(string busId);
        ValidationResult RemoveAllGpses();
        IEnumerable<Gps> GetNerbyBusesGps(decimal latitude, decimal longitude, decimal variance, string line);
    }
}
