using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;
using Ikuzo.Domain.Interfaces.Services;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Services
{
    public class GpsService : IGpsService
    {
        private readonly IGpsRepository _gpsRepository;

        public GpsService(IGpsRepository gpsRepository)
        {
            _gpsRepository = gpsRepository;
        }

        public Gps GetBusGps(string externalBusId)
        {
            return _gpsRepository.GetBusGps(externalBusId);
        }

        public void CreateGpses(IEnumerable<Gps> gpses)
        {
            _gpsRepository.GpsBulkInsert(gpses);
        }

        public ValidationResult RemoveGpsesFromLine(string lineId)
        {
            var result = new ValidationResult();
            try
            {
                _gpsRepository.RemoveFromLine(lineId);
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }

        public ValidationResult RemoveAllGpses()
        {
            var result = new ValidationResult();
            try
            {
                _gpsRepository.RemoveAll();
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }

        public ValidationResult RemoveGpsesFromBus(string busId)
        {
            var result = new ValidationResult();
            try
            {
                _gpsRepository.RemoveFromLine(busId);
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }

        public IEnumerable<Gps> GetNerbyBusesGps(decimal latitude, decimal longitude, decimal variance, string lineId)
        {
            var gpses  = string.IsNullOrEmpty(lineId) ? _gpsRepository.GetNerbyBusesGps(latitude, longitude, variance).ToList() :
                                                   _gpsRepository.GetNerbyBusesGpsFromLine(lineId).ToList();
            return gpses;
        }
    }
}
