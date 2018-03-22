using System;
using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using Ikuzo.Application.Interfaces;
using Ikuzo.Application.ViewModels.Bus; 
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Application.App
{
    public class BusApp : IBusApp
    {
        private readonly IBusService _busService;

        public BusApp(IBusService busService)
        {
            _busService = busService;
        }

        public IEnumerable<BusIndex> GetBuses()
        {
            var buses = _busService.GetAllBuses().ToList();

            var modelbuses = Mapper.Map<List<Bus>, List<BusIndex>>(buses);

            return modelbuses;
        }

        public BusDetails GetBus(string externalLineId)
        {
            var bus = _busService.Details(externalLineId);

            var modelBuses = Mapper.Map<Bus,BusDetails>(bus);

            return modelBuses;
        }

        public BusDetails GetBus(int lineId)
        {
            throw new NotImplementedException();
        }
    }
}
