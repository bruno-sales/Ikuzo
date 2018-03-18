using System.Collections.Generic;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces.CrossCuttings
{
    public interface IRioBusRepository
    {
        IEnumerable<RioBusLine> GetAllLines();
    }
}
