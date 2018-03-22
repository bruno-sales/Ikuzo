using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Application.Interfaces;
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

        public IEnumerable<Line> GetLines()
        {
            return _lineService.GetAllLines().ToList();
        }

        public Line GetLine(string externalLineId)
        {
            return _lineService.Details(externalLineId);
        }

        public Line GetLine(int lineId)
        {
           return _lineService.Details(lineId);
        }
    }
}
