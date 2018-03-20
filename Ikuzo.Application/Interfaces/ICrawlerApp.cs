using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Application.Interfaces
{
    public interface ICrawlerApp
    {
        ValidationResult SyncGps();
        ValidationResult SyncLines();
        ValidationResult SyncBuses();
    }
}
