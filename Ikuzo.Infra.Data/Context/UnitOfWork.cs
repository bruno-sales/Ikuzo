using System;
using System.Data.Entity.Validation;
using System.Linq;
using Ikuzo.Domain;
using Ikuzo.Domain.Interfaces;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Infra.Data.Context
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;

        public UnitOfWork(Context billingContext)
        {
            _context = billingContext;
        }

        public ValidationResult Commit()
        {
            var commitValidation = new ValidationResult();
            try
            {
                if (_context.SaveChanges() < 1)
                {
                    commitValidation.AddError(new ValidationError("Ocorreu um problema ao salvar"));
                }
            }
            catch (DbEntityValidationException ex)
            {
                var erros = "";
                foreach (var error in ex.EntityValidationErrors)
                {
                    erros += "Entity - " + error.Entry.Entity.GetType().Name;
                    erros += "State - " + error.Entry.State;
                    erros = error.ValidationErrors.Aggregate(erros, (current, validationError) => current + string.Format("\tProperty: {0}, Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                } 
                commitValidation.AddError(new ValidationError(erros));
            }
            return commitValidation;
        }

        public void Dispose()
        {
            _context.Dispose();

        }

        ~UnitOfWork()
        {
            Dispose();
        }
    }
}
