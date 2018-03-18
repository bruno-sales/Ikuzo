using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface ILineService
    {
        IEnumerable<Line> CreateLines(IEnumerable<Line> line);
        Line Edit(Line line);
        Line Details(int lineId);
        Line Details(string externalId);
    }
}
