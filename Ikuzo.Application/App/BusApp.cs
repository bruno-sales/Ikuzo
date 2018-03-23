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
        private readonly IGpsService _gpsService;

        public BusApp(IBusService busService, IGpsService gpsService)
        {
            _busService = busService;
            _gpsService = gpsService;
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

            if (bus == null || modelBuses == null)
                return modelBuses;

            var gps = _gpsService.GetBusGps(externalLineId);

            var direction = "West";

            if (gps.Direction > 315 && gps.Direction <= 45)
            {
                direction = "North";
            }
            else if (gps.Direction > 45 && gps.Direction <= 135)
            {
                direction = "East";
            }
            else if(gps.Direction > 135 && gps.Direction <= 225)
            {
                direction = "South";
            }

            modelBuses.Gps = new BusGps()
            {
                Latitude = gps.Latitude,
                Longitude = gps.Longitude,
                Direction = direction
            };

            return modelBuses;
        }

        public BusDetails GetBus(int lineId)
        {
            throw new NotImplementedException();
        }
    }
}
