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
                .Member(dest => dest.Line, i => i.ExternalId)
                .Member(dest => dest.Name, i => i.Description)
                .Member(dest => dest.LastUpdateDate, i => i.CreateDate);

            Mapper.Register<Line, LineDetails>()
                .Member(dest => dest.Line, i => i.ExternalId)
                .Member(dest => dest.Name, i => i.Description)
                .Member(dest => dest.LastUpdateDate, i => i.CreateDate);

            Mapper.Register<Bus, LineBus>()
                .Member(dest => dest.Bus, i => i.ExternalId)
                .Member(dest => dest.LastUpdateDate, i => i.CreateDate);

            Mapper.Register<Bus, BusIndex>()
                .Member(dest => dest.Bus, i => i.ExternalId)
                .Member(dest => dest.LastUpdateDate, i => i.CreateDate)
                .Member(dest => dest.Line, i => i.Line.ExternalId);
        }
    }
}
