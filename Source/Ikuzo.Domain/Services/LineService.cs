﻿using System.Collections.Generic;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Domain.Services
{
    public class LineService : ILineService
    {
        private readonly ILineRepository _lineRepository;
        private readonly IItineraryRepository _itineraryRepository;

        public LineService(ILineRepository lineRepository, IItineraryRepository itineraryRepository)
        {
            _lineRepository = lineRepository;
            _itineraryRepository = itineraryRepository;
        }

        public void CreateLines(IEnumerable<Line> lines)
        {
             _lineRepository.LineBulkInsert(lines); 
        }

        public Line Edit(Line line)
        {
            var editedLine = _lineRepository.Edit(line);

            return editedLine;
        }

        public Line Get(string lineId)
        {
            var line = _lineRepository.Get(lineId);

            return line;
        } 

        public Line Details(string lineId)
        {
            var line = _lineRepository.Details(lineId);

            return line;
        }

        public IEnumerable<Line> GetAllLines()
        {
            var lines = _lineRepository.GetAll().OrderBy(i=>i.LineId).ToList();

            return lines;
        }

        public IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal distance)
        {
            return _itineraryRepository.GetLocalLines(latitude, longitude, distance).ToList();
        }

        public IEnumerable<Itinerary> GetLineItineraries(string lineId)
        {
            var itineraries = _itineraryRepository.GetLineItinerary(lineId).ToList();

            return itineraries;
        }
    }
}
