using ExpressMapper;
using Ikuzo.Application.ViewModels.Line;
using Ikuzo.Application.ViewModels.Modal;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Application.Configurations
{
    public class DomainToViewModel
    {
        public void Initialize()
        {
            Mapper.Register<Line, LineIndex>()
                .Member(dest => dest.Line, i => i.LineId)
                .Member(dest => dest.Name, i => i.Description);

            Mapper.Register<Line, LineDetails>()
                .Member(dest => dest.Line, i => i.LineId)
                .Member(dest => dest.Name, i => i.Description);

            Mapper.Register<Modal, LineModal>()
                .Member(dest => dest.Modal, i => i.ModalId);

            Mapper.Register<Modal, ModalIndex>()
                .Member(dest => dest.Modal, i => i.ModalId)
                .Member(dest => dest.Line, i => i.Line.LineId);

            Mapper.Register<Modal, ModalDetails>()
                .Member(dest => dest.Modal, i => i.ModalId)
                .Member(dest => dest.Line, i => i.Line.LineId);

            Mapper.Register<Itinerary, LineItinerary>();
        }
    }
}
