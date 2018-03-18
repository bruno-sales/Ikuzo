using System.Linq;
using Ikuzo.Application.Interfaces;
using Ikuzo.Domain.Interfaces.CrossCuttings;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Application.App
{
    public class CrawlerApp : ICrawlerApp
    {
        private readonly IRioBusRepository _riobusRepository;

        public CrawlerApp(IRioBusRepository riobusRepository)
        {
            _riobusRepository = riobusRepository;
        }

        public ValidationResult SyncLines()
        {
            var validation = new ValidationResult();

            var lines = _riobusRepository.GetAllLines().ToList();

            return validation;
        }
    }
}
