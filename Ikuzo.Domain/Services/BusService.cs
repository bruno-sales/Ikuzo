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

        public BusService(IBusRepository busRepository)
        {
            _busRepository = busRepository;
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
        
        public Bus Details(int busId)
        {
            var bus = _busRepository.Get(busId);

            return bus;
        }

        public Bus Details(string externalId)
        {
            var bus = _busRepository.GetWhere(i => string.Equals(i.ExternalId.ToLower(), externalId.ToLower())).FirstOrDefault();

            return bus;
        }
    }
}
