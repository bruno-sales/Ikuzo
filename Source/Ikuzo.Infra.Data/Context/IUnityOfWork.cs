using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Infra.Ioc
{
    public interface IUnitOfWork
    {
        ValidationResult Commit();
    }
}
