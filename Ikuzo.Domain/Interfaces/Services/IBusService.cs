using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IBusService
    {
        IEnumerable<Bus> CreateBuses(IEnumerable<Bus> bus);
        Bus Edit(Bus bus);
        Bus Details(int busId);
        Bus Details(string externalId);
    }
}
