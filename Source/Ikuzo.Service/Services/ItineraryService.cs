using System;
using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;
using Ikuzo.Infra.Data.Interfaces;
using Ikuzo.Service.Interfaces;

namespace Ikuzo.Service.Services
{
    public class ItineraryService : IItineraryService
    {
        private readonly IItineraryRepository _itineraryRepository;

        public ItineraryService(IItineraryRepository itineraryRepository)
        {
            _itineraryRepository = itineraryRepository;
        }

        public void CreateItineraries(IEnumerable<Itinerary> itineraries)
        {
             _itineraryRepository.ItineraryBulkInsert(itineraries);
        }

        public ValidationResult RemoveItinerariesFromLine(string lineId)
        {
            var result = new ValidationResult();
            try
            {
                _itineraryRepository.RemoveFromLine(lineId);
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }

        public ValidationResult RemoveAllItineraries()
        {
            var result = new ValidationResult();
            try
            {
                _itineraryRepository.RemoveAll();
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }
    }
}
