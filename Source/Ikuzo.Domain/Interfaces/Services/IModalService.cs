using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces.Services
{
    public interface IModalService
    {
        IEnumerable<Modal> GetAllModals();
        void CreateModals(IEnumerable<Modal> modals);
        Modal Edit(Modal modal); 
        Modal Get(string modalId); 
        Modal Details(string modalId);
        ValidationResult RemoveModalsFromLine(string lineId);
    }
}
