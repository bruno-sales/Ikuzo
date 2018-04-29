namespace Ikuzo.Domain.ValueObjects
{
    public class ValidationError
    {
        public string ErrorMessage { get; set; }

        public ValidationError(string message)
        {
            ErrorMessage = message;
        }
    }
}
