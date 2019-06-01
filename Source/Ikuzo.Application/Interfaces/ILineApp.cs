using System.Collections.Generic;
using Ikuzo.Application.ViewModels.Line; 

namespace Ikuzo.Application.Interfaces
{
    public interface ILineApp
    {
        IEnumerable<LineIndex> GetLines();
        LineDetails GetLine(string lineId);
        IEnumerable<LineIndex> GetLocalLines(decimal latitude, decimal longitude, decimal? distance);
        LineItinerary GetLineItinerary(string lineId);
    }
}
