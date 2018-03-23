using System;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IBusRepository : IBaseRepository<Bus>
    {
        Bus Details(Guid busId);
        Bus Details(string externalBusId);
    }
}
