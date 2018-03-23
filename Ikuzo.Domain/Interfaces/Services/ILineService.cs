using System;
using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface ILineService
    {
        IEnumerable<Line> CreateLines(IEnumerable<Line> line);
        IEnumerable<Line> GetAllLines();
        Line Get(Guid lineId);
        Line Get(string externalId);
        Line Details(Guid lineId);
        Line Details(string externalId);
        Line Edit(Line line);
    }
}
