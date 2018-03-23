using System;
using System.Collections.Generic;
using Ikuzo.Application.ViewModels.Line; 

namespace Ikuzo.Application.Interfaces
{
    public interface ILineApp
    {
        IEnumerable<LineIndex> GetLines();
        LineDetails GetLine(string externalLineId);
        LineDetails GetLine(Guid lineId);
    }
}
