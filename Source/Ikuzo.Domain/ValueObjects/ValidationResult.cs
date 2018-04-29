using System.Collections.Generic;
using System.Linq;

namespace Ikuzo.Domain.ValueObjects
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<ValidationError>();
        }

        //Sucesso
        public bool Success => !Errors.Any();

        //Listagem de erros
        public IList<ValidationError> Errors { get; }

        public void AddError(ValidationError validationError)
        {
            Errors.Add(validationError);
        }

        public void AddError(ValidationResult validationResult)
        {
            foreach (var validationError in validationResult.Errors)
            {
                AddError(validationError);
            }
        }
    }
}
