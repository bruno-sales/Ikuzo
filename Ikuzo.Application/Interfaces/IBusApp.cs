using System.Collections.Generic; 
using Ikuzo.Application.ViewModels.Bus;

namespace Ikuzo.Application.Interfaces
{
    public interface IBusApp
    {
        IEnumerable<BusIndex> GetBuses();
        BusDetails GetBus(string externalLineId);
        BusDetails GetBus(int lineId);
    }
}
