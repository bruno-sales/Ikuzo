using Ikuzo.Application.App;
using Ikuzo.Application.Interfaces; 
using Ikuzo.Domain.Interfaces.CrossCuttings;
using Ikuzo.Domain.Interfaces.Repositories;
using Ikuzo.Domain.Interfaces.Services;
using Ikuzo.Domain.Services;
using Ikuzo.Infra.Data.Context;
using Ikuzo.Infra.Data.Repository;
using Ikuzo.Infra.DataRio;
using Ikuzo.Infra.RioBus;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Ikuzo.Infra.Ioc
{
    public class MyContainer
    {
        public Container Initialize()
        {
            //Insert Modules
            var container = new Container();
            
            var asyncScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultScopedLifestyle = asyncScopedLifestyle;

            //UOW
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            //Context
            container.Register<Context>(Lifestyle.Scoped);

            //External
            container.Register<IRioBusRepository, RioBusRepository>(Lifestyle.Scoped);
            container.Register<IDataRioRepository, DataRioRepository>(Lifestyle.Scoped);

            //Repositories
            container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>), Lifestyle.Scoped);
            container.Register<IBusRepository, BusRepository>(Lifestyle.Scoped);
            container.Register<ILineRepository, LineRepository>(Lifestyle.Scoped);
            container.Register<IGpsRepository, GpsRepository>(Lifestyle.Scoped);

            //Services
            container.Register<IBusService, BusService>(Lifestyle.Scoped);
            container.Register<ILineService, LineService>(Lifestyle.Scoped);
            container.Register<IGpsService, GpsService>(Lifestyle.Scoped);

            //ReadOnly
            //container.Register<BaseReadOnlyRepository, BaseReadOnlyRepository>(Lifestyle.Scoped);

            //Application 
            container.Register<ICrawlerApp, CrawlerApp>(Lifestyle.Scoped);
            container.Register<ILineApp, LineApp>(Lifestyle.Scoped);
            container.Register<IBusApp, BusApp>(Lifestyle.Scoped);

            return container;
        }
    }
}
