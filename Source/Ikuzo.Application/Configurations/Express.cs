namespace Ikuzo.Application.Configurations
{
    public class Express
    {
        public void Mapper()
        {
            var domainToViewModel = new DomainToViewModel();
            domainToViewModel.Initialize();
        }
    }
}
