using Ikuzo.Domain.Interfaces;
using Ikuzo.Infra.Data.Context;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Ikuzo.Infra.Ioc
{
    public class MyContainer
    {
        public Container Initialize()
        {
            //Inserir Modules
            var container = new Container();
            
            var asyncScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultScopedLifestyle = asyncScopedLifestyle;

            //UOW
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            //Contexto
            container.Register<Context>(Lifestyle.Scoped);

            //Entity Framework
            //container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>), Lifestyle.Scoped);
            //container.Register<IBillingEventCountRepository, BillingEventCountRepository>(Lifestyle.Scoped);


            //Services
            //container.Register<IEventTypeService, EventTypeService>(Lifestyle.Scoped);


            //ReadOnly
            //container.Register<BaseReadOnlyRepository, BaseReadOnlyRepository>(Lifestyle.Scoped);
         
            //Application 
            //container.Register<IBilingEventCountApp, BilingEventCountApp>(Lifestyle.Scoped);

            return container;
        }
    }
}
