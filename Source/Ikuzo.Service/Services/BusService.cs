using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;
using Ikuzo.Infra.Data.Interfaces;
using Ikuzo.Service.Interfaces;

namespace Ikuzo.Service.Services
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;

        public BusService(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            var buses = _busRepository.GetAll().OrderBy(i => i.LineId).ToList();

            return buses;
        }

        public void CreateBuses(IEnumerable<Bus> bus)
        {
             _busRepository.BusBulkInsert(bus); 
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

        public ValidationResult RemoveBusesFromLine(string lineId)
        {
            var result = new ValidationResult();
            try
            {
                _busRepository.RemoveFromLine(lineId);
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }
    }
}
