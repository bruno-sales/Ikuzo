using System;
using System.Collections.Generic;
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

        public IEnumerable<Gps> CreateGpses(IEnumerable<Gps> gpses)
        {
            return _gpsRepository.Create(gpses);
        }
        
        public ValidationResult RemoveGpsesFromLine(string externalLineId)
        {
            var result = new ValidationResult();
            try
            {
                _gpsRepository.RemoveFromLine(externalLineId);
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }
    }
}
