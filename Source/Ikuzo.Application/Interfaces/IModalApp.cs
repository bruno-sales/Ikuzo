using System.Collections.Generic;
using Ikuzo.Application.ViewModels.Modal;

namespace Ikuzo.Application.Interfaces
{
    public interface IModalApp
    {
        IEnumerable<ModalIndex> GetModals();
        ModalDetails GetModal(string modalId);
        IEnumerable<ModalNearbyDetails> GetNearbyModals(decimal latitude, decimal longitude, decimal? distance,
            string lineId);
    }
}
