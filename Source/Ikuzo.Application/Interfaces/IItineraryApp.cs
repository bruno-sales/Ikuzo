using System.Collections.Generic;
using Ikuzo.Application.ViewModels.Line; 

namespace Ikuzo.Application.Interfaces
{
    public interface IItineraryApp
    {
        IEnumerable<LineIndex> GetLocalToDestinyLines(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2, List<string> tags, decimal distance);
    }
}
