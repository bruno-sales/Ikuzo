using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;
using Ikuzo.Domain.Interfaces.Services;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Services
{
    public class ModalService : IModalService
    {
        private readonly IModalRepository _modalRepository;

        public ModalService(IModalRepository modalRepository)
        {
            _modalRepository = modalRepository;
        }

        public IEnumerable<Modal> GetAllModals()
        {
            var modals = _modalRepository.GetAll().OrderBy(i => i.LineId).ToList();

            return modals;
        }

        public void CreateModals(IEnumerable<Modal> modals)
        {
             _modalRepository.ModalBulkInsert(modals); 
        }

        public Modal Edit(Modal modal)
        {
            var editedModal = _modalRepository.Edit(modal);

            return editedModal;
        } 

        public Modal Get(string modalId)
        {
            var modal = _modalRepository.GetWhere(i => string.Equals(i.ModalId.ToLower(), modalId.ToLower())).FirstOrDefault();

            return modal;
        } 

        public Modal Details(string modalId)
        {
            var modal = _modalRepository.Details(modalId);

            return modal;
        }

        public ValidationResult RemoveModalsFromLine(string lineId)
        {
            var result = new ValidationResult();
            try
            {
                _modalRepository.RemoveFromLine(lineId);
            }
            catch (Exception e)
            {
                result.AddError(new ValidationError(e.Message));
            }

            return result;
        }
    }
}
