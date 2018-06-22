using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.CrossCuttings
{
    public interface IDataRioRepository
    {
        IEnumerable<Gps> GetGpsInformation();
    }
}
