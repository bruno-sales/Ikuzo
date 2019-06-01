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

        public Gps GetModalGps(string externalModalId)
        {
            return _gpsRepository.GetModalGps(externalModalId);
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

        public IEnumerable<Gps> GetNerbyModalsGps(decimal latitude, decimal longitude, decimal distance, string lineId)
        {
            var gpses  = string.IsNullOrEmpty(lineId) ? _gpsRepository.GetNerbyModalsGps(latitude, longitude, distance).ToList() :
                                                   _gpsRepository.GetNerbyModalsGpsFromLine(lineId).ToList();
            return gpses;
        }
    }
}
