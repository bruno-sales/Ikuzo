using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface ILineService
    {
        Line Edit(Line line);
        void CreateLines(IEnumerable<Line> lines);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, List<int> tags, decimal distance); 
        Line Get(string lineId); 
        Line Details(string lineId); 
    }
}
