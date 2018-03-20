using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IGpsService
    {
        IEnumerable<Gps> CreateGpses(IEnumerable<Gps> gpses);
        ValidationResult RemoveGpsesFromLine(string lineId);
    }
}
