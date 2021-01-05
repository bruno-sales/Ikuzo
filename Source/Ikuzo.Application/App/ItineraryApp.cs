using System;
using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using Ikuzo.Application.Interfaces;
using Ikuzo.Application.ViewModels.Line;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Helpers;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Application.App
{
    public class ItineraryApp : IItineraryApp
    {
        private readonly IItineraryService _itineraryService;

        public ItineraryApp(IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        public IEnumerable<LineIndex> GetLocalToDestinyLines(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2, List<string> tags, decimal distance)
        {
            var enumTags = EnumHelper.GetTagIds(tags);

            var lines = _itineraryService.GetLocalToDestinyLines(latitude1, longitude1, latitude2, longitude2, enumTags, distance).ToList();


            /*
            foreach (var line in lines)
            {
                var itineraries = _itineraryService.GetLineItineraries(line.LineId).ToList();

                Itinerary closerToStartItinerary = null;
                Itinerary closerToEndItinerary = null;

                var distanceToStart = int.MaxValue;
                var distanceToEnd = int.MaxValue;

                foreach (var itinerary in itineraries)
                {
                    var startDistance = GpsHelper.DistanceBetweenCoordenates(latitude1, longitude1, itinerary.Latitude,
                        itinerary.Longitude);

                    var endDistance = GpsHelper.DistanceBetweenCoordenates(latitude2, longitude2, itinerary.Latitude,
                        itinerary.Longitude);

                    if (distanceToStart > startDistance)
                    {
                        distanceToStart = startDistance;
                        closerToStartItinerary = itinerary;
                    }

                    if (distanceToEnd > endDistance)
                    {
                        distanceToEnd = endDistance;
                        closerToEndItinerary = itinerary;
                    }
                }

                
                if (closerToStartItinerary.Sequence < closerToEndItinerary.Sequence && closerToStartItinerary.Returning == closerToEndItinerary.Returning)
                {

                    var totalDistance = itineraries
                        .Where(i => i.Sequence >= closerToStartItinerary.Sequence &&
                                    i.Sequence <= closerToEndItinerary.Sequence &&
                                    i.Returning == closerToStartItinerary.Returning).Sum(i => i.DistanceToNext);
                }
                else
                {
                    var totalDistance = itineraries
                        .Where(i => i.Sequence >= closerToEndItinerary.Sequence &&
                                    i.Sequence <= closerToStartItinerary.Sequence &&
                                    i.Returning == closerToStartItinerary.Returning).Sum(i => i.DistanceToNext);
                }

            }
            */

            var modelLines = Mapper.Map<List<Line>, List<LineIndex>>(lines);

            return modelLines;
        }
    }
}
