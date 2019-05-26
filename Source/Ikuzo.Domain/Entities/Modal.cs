using System;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Ikuzo.Domain.Entities
{
    public class Modal
    {
        public Modal()
        {
            LastUpdateDate = DateTime.UtcNow;
        }

        public Modal(string modalId, string lineId)
        {
            ModalId = modalId;
            LineId = lineId;
            LastUpdateDate = DateTime.UtcNow;
        }

        public string ModalId { get; set; }
        public string LineId { get; set; }
        public virtual Line Line { get; set; }
        [NotMapped]
        public Gps Gps { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
