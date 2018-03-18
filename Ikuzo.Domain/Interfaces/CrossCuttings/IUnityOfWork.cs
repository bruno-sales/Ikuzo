using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces.CrossCuttings
{
    public interface IUnitOfWork
    {
        ValidationResult Commit();
    }
}
