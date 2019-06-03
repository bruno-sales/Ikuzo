using System;
using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;
using Ikuzo.Domain.Interfaces.Services;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Services
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

        public IEnumerable<Line> GetLocalToDestinyLines(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2, decimal distance)
        {
            var lines = _itineraryRepository.GetLocalToDestinyLines(latitude1, longitude1, latitude2, longitude2, distance);

            return lines;
        }
    }
}
