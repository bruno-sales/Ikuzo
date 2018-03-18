using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace Ikuzo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Habilitar requisições de qualquer lugar
            var corsAttr = new EnableCorsAttribute("*", "*", "*");

            // Web API configuration and services
            config.EnableCors(corsAttr);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void ResponseFormater(HttpConfiguration config)
        {
            var formatters = config.Formatters;
            //Remover formato XML
            formatters.Remove(formatters.XmlFormatter);

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.Formatting = Formatting.Indented;
        }
    }
}
