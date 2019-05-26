using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Service.Interfaces
{
    public interface IBusService
    {
        IEnumerable<Bus> GetAllBuses();
        void CreateBuses(IEnumerable<Bus> bus);
        Bus Edit(Bus bus); 
        Bus Get(string busId); 
        Bus Details(string busId);
        ValidationResult RemoveBusesFromLine(string lineId);
    }
}
