using ExpressMapper;
using Ikuzo.Application.ViewModels.Bus;
using Ikuzo.Application.ViewModels.Line;
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

            Mapper.Register<Bus, LineBus>()
                .Member(dest => dest.Bus, i => i.BusId);

            Mapper.Register<Bus, BusIndex>()
                .Member(dest => dest.Bus, i => i.BusId)
                .Member(dest => dest.Line, i => i.Line.LineId);

            Mapper.Register<Bus, BusDetails>()
                .Member(dest => dest.Bus, i => i.BusId)
                .Member(dest => dest.Line, i => i.Line.LineId);

            Mapper.Register<Itinerary, LineItinerary>();
        }
    }
}
