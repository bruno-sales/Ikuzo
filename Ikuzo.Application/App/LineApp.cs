using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using Ikuzo.Application.Interfaces;
using Ikuzo.Application.ViewModels.Line;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Application.App
{
    public class LineApp : ILineApp
    {
        private readonly ILineService _lineService;

        public LineApp(ILineService lineService)
        {
            _lineService = lineService;
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

        public IEnumerable<LineIndex> GetLocalLines(decimal latitude, decimal longitude, decimal variance)
        {
            var lines = _lineService.GetLocalLines(latitude, longitude, variance).ToList();

            var modelLines = Mapper.Map<List<Line>, List<LineIndex>>(lines);

            return modelLines;
        }

    }
}
