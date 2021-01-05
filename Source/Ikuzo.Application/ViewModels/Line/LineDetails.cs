using System;
using System.Collections.Generic; 

namespace Ikuzo.Application.ViewModels.Line
{
    public class LineDetails
    {
        public LineDetails()
        {
            Tags = new List<string>();
            Modals = new List<LineModal>();
        }

        public string Line { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<LineModal> Modals { get; set; }
    }
}
