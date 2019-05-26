using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IModalRepository : IBaseRepository<Modal>
    {
        Modal Details(string modalId);
        void RemoveFromLine(string lineId);
        void ModalBulkInsert(IEnumerable<Modal> modals);
    }
}
