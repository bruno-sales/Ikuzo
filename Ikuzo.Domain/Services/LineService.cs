using System.Collections.Generic;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Domain.Services
{
    public class LineService : ILineService
    {
        private readonly ILineRepository _lineRepository;

        public LineService(ILineRepository lineRepository)
        {
            _lineRepository = lineRepository;
        }

        public IEnumerable<Line> CreateLines(IEnumerable<Line> line)
        {
            var createdLines = _lineRepository.Create(line);

            return createdLines;
        }

        public Line Edit(Line line)
        {
            var editedLine = _lineRepository.Edit(line);

            return editedLine;
        }


        public Line Details(int lineId)
        {
            var line = _lineRepository.Details(lineId);

            return line;
        }

        public Line Details(string externalId)
        {
            var line = _lineRepository.GetWhere(i => string.Equals(i.ExternalId.ToLower(), externalId.ToLower())).FirstOrDefault();

            return line;
        }
    }
}
