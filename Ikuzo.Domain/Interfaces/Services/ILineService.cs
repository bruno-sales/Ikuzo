using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface ILineService
    {
        Line Edit(Line line);
        IEnumerable<Line> CreateLines(IEnumerable<Line> line);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal variance);
        Line Get(string lineId); 
        Line Details(string lineId); 
    }
}
