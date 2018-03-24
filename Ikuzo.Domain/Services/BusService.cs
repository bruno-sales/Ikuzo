using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Domain.Services
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;
        private readonly IGpsRepository _gpsRepository;

        public BusService(IBusRepository busRepository, IGpsRepository gpsRepository)
        {
            _busRepository = busRepository;
            _gpsRepository = gpsRepository;
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            var buses = _busRepository.GetAll().OrderBy(i => i.LineId).ToList();

            return buses;
        }

        public IEnumerable<Bus> CreateBuses(IEnumerable<Bus> bus)
        {
            var createdBuses = _busRepository.Create(bus);

            return createdBuses;
        }

        public Bus Edit(Bus bus)
        {
            var editedBus = _busRepository.Edit(bus);

            return editedBus;
        } 

        public Bus Get(string busId)
        {
            var bus = _busRepository.GetWhere(i => string.Equals(i.BusId.ToLower(), busId.ToLower())).FirstOrDefault();

            return bus;
        } 

        public Bus Details(string busId)
        {
            var bus = _busRepository.Details(busId);

            return bus;
        }
    }
}
