using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IItineraryRepository : IBaseRepository<Itinerary>
    {
        void RemoveFromLine(string lineId);
    }
}
