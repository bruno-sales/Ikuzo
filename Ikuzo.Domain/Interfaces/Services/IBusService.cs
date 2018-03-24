using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IBusService
    {
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> CreateBuses(IEnumerable<Bus> bus);
        Bus Edit(Bus bus); 
        Bus Get(string busId); 
        Bus Details(string busId);
    }
}
