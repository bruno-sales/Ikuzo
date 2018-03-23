using System;
using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IBusService
    {
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> CreateBuses(IEnumerable<Bus> bus);
        Bus Edit(Bus bus);
        Bus Get(Guid busId);
        Bus Get(string externalId);
        Bus Details(Guid busId);
        Bus Details(string externalId);
    }
}
