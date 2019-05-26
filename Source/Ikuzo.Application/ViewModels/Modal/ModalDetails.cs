using System;

namespace Ikuzo.Application.ViewModels.Modal
{
    public class ModalDetails
    {
        public string Modal { get; set; }
        public string Line { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public ModalGps Gps { get; set; }
    }
}
