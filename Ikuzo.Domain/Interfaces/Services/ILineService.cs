using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface ILineService
    {
        IEnumerable<Line> CreateLines(IEnumerable<Line> line);
        IEnumerable<Line> GetAllLines();
        Line Get(int lineId);
        Line Get(string externalId);
        Line Details(int lineId);
        Line Details(string externalId);
        Line Edit(Line line);
    }
}
