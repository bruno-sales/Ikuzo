using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Application.Interfaces
{
    public interface ILineApp
    {
        IEnumerable<Line> GetLines();
        Line GetLine(string externalLineId);
        Line GetLine(int lineId);
    }
}
