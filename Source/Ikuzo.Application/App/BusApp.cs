using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ExpressMapper;
using Ikuzo.Application.Interfaces;
using Ikuzo.Application.ViewModels.Bus;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Helpers;
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

        public BusDetails GetBus(string busId)
        {
            var bus = _busService.Details(busId);

            var modelBuses = Mapper.Map<Bus, BusDetails>(bus);

            if (bus == null || modelBuses == null)
                return modelBuses;

            var gps = _gpsService.GetBusGps(busId);
            
            modelBuses.Gps = new BusGps()
            {
                Latitude = gps.Latitude,
                Longitude = gps.Longitude,
                Direction = gps.Direction.ToString()
            };

            return modelBuses;
        }

        public IEnumerable<BusNearbyDetails> GetNearbyBuses(decimal latitude, decimal longitude, decimal variance, string lineId)
        {
            var buses = new List<BusNearbyDetails>(); 

            var gpses = _gpsService.GetNerbyBusesGps(latitude, longitude, variance, lineId).ToList();

            foreach (var gps in gpses)
            {
                var distance = GpsHelper.DistanceBetweenCoordenates(latitude, longitude, gps.Latitude, gps.Longitude);
                var busAvgSpeed = Convert.ToDouble(ConfigurationManager.AppSettings["BusAvgSpeed"] ?? "4.2");

                var minutesToArrive = (distance / busAvgSpeed) / 60.0;
                var bus = new BusNearbyDetails()
                {
                    Bus = gps.BusId,
                    Line = gps.LineId,
                    Distance = distance,
                    MinutesToArrive = Convert.ToInt32(minutesToArrive),
                    LastUpdateDate = gps.LastUpdateDate,
                    TimeStamp = gps.Timestamp,
                    Gps = new BusGps()
                    {
                        Latitude = gps.Latitude,
                        Longitude = gps.Longitude,
                        Direction = gps.Direction.ToString()
                    }
                };

                buses.Add(bus);
            }

            return buses.OrderBy(i=>i.Distance);
        }
    }
}
