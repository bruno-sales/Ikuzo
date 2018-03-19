using System.Collections.Generic;
using Ikuzo.Domain.ValueObjects.RioBus;

namespace Ikuzo.Domain.Interfaces.CrossCuttings
{
    public interface IRioBusRepository
    {
        IEnumerable<RbLine> GetAllLines();
        IEnumerable<RbBus> GetBusesInfoFromLine(string externalId);
    }
}
