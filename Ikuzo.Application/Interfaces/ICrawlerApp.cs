using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Application.Interfaces
{
    public interface ICrawlerApp
    {
        ValidationResult SyncLines();
    }
}
