using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using Ikuzo.Application.Interfaces;
using Ikuzo.Application.ViewModels.Line;
using Ikuzo.Domain.Entities;
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

        public IEnumerable<LineIndex> GetLocalToDestinyLines(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2, decimal distance)
        {
            var lines = _itineraryService.GetLocalToDestinyLines(latitude1, longitude1, latitude2, longitude2,distance).ToList();

            var modelLines = Mapper.Map<List<Line>, List<LineIndex>>(lines);

            return modelLines;
        }
    }
}
