using System;
using System.Web.Http;
using Ikuzo.Infra.Ioc;
using SimpleInjector.Integration.WebApi;

namespace Ikuzo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(WebApiConfig.ResponseFormater);

            //Inicializando configuração do Simple Injector
            var container = new MyContainer().Initialize();

            // Verificando o simple injector
            container.Verify();

            //Fazer criação de dependency resolver
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("Server");
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }
    }
}
