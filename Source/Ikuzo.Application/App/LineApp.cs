using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ExpressMapper;
using Ikuzo.Application.Interfaces;
using Ikuzo.Application.ViewModels.Itinerary;
using Ikuzo.Application.ViewModels.Line;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Application.App
{
    public class LineApp : ILineApp
    {
        private readonly ILineService _lineService;
        private readonly IItineraryService _itineraryService;

        public LineApp(ILineService lineService, IItineraryService itineraryService)
        {
            _lineService = lineService;
            _itineraryService = itineraryService;
        }

        public IEnumerable<LineIndex> GetLines()
        {
            var lines = _lineService.GetAllLines().ToList();

            var modelLines = Mapper.Map<List<Line>, List<LineIndex>>(lines);

            return modelLines;
        }

        public LineDetails GetLine(string lineId)
        {
            var modelLine = Mapper.Map<Line, LineDetails>(_lineService.Details(lineId));

            return modelLine;
        }

        public IEnumerable<LineIndex> GetLocalLines(decimal latitude, decimal longitude, decimal? distance)
        {
            if (distance == null)
                distance = Convert.ToDecimal(ConfigurationManager.AppSettings["DefaultDistance"] ?? "200");

            var lines = _lineService.GetLocalLines(latitude, longitude, distance.Value).ToList();

            var modelLines = Mapper.Map<List<Line>, List<LineIndex>>(lines);

            return modelLines;
        }

        public LineItinerary GetLineItinerary(string lineId)
        {
            var line = _lineService.Get(lineId);

            if (line == null) return null;

            var itineraries = _itineraryService.GetLineItineraries(lineId).ToList();
            var itineraryIndex = Mapper.Map<List<Itinerary>, List<ItineraryIndex>>(itineraries);
            
            var lineItinerary = new LineItinerary { Line = line.LineId, Name = line.Description };
            lineItinerary.Itineraries.AddRange(itineraryIndex);

            return lineItinerary;
        }
    }
}

