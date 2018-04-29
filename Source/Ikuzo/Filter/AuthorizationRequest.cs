using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Ikuzo.Filter
{
    public class AuthorizationRequest : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (VerifyHeader(actionContext))
                return;

            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Acesso não autorizado");
            actionContext.Response = response;
        }

        private static bool VerifyHeader(HttpActionContext actionContext)
        {
            var accessKeyHeader = actionContext.Request.Headers.FirstOrDefault(i => i.Key == "AccessKey");
            if (accessKeyHeader.Value == null)
            {
                return false;
            }

            var value = accessKeyHeader.Value.ToArray()[0];

            // Variável que conterá a chave de acesso recebida.
            var headerAccessKeyParse = Guid.TryParse(value, out var accessKey);

            var billingAccessKeyParse = Guid.TryParse(ConfigurationManager.AppSettings["AccessKey"], out var accessKeyWebConfig);

            return (accessKey == accessKeyWebConfig) && headerAccessKeyParse && billingAccessKeyParse;
        }
    }
}