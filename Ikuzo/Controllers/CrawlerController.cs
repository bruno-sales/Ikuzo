using System.Net; 
using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.Domain.ValueObjects;
using Ikuzo.Filter;

namespace Ikuzo.Controllers
{
    [AuthorizationRequest]
    [RoutePrefix("v1/api/crawler")]
    public class CrawlerController : ApiController
    {

        /* 1- Quais onibus passam aqui (OK)
           1.1- Disponibilizar cadastro de itinerarios
           1.2- Listar os onibus que tão quase chegando no ponto (OK)
           2- Como chegar num destino com os onibus daqui (OK - Usar o get local com um range maior)
           Plus- Qual o melhor onibus pra chegar no destino 
        */
        private readonly ICrawlerApp _crawlerApp;

        public CrawlerController(ICrawlerApp crawlerApp)
        {
            _crawlerApp = crawlerApp;
        }

        [HttpPost]
        [Route("gps")]
        public IHttpActionResult SyncGps()
        {
            var result = new ValidationResult();

            result.AddError(_crawlerApp.SyncGps());

            if (result.Success)
                return Ok();
            else
                return Content(HttpStatusCode.InternalServerError, result.Errors);

        }

        [HttpPost]
        [Route("lines")]
        public IHttpActionResult SyncLines()
        {
            var result = new ValidationResult();

            result.AddError(_crawlerApp.SyncLines());

            if (result.Success)
                return Ok();
            else
                return Content(HttpStatusCode.InternalServerError, result.Errors);

        }


        [HttpPost]
        [Route("buses")]
        public IHttpActionResult SyncBuses()
        {
            var result = new ValidationResult();

            result.AddError(_crawlerApp.SyncBuses());

            if (result.Success)
                return Ok();
            else
                return Content(HttpStatusCode.InternalServerError, result.Errors);
        }

        [HttpPost]
        [Route("itineraries")]
        public IHttpActionResult SyncItineraries()
        {
            var result = new ValidationResult();

            result.AddError(_crawlerApp.SyncItineraries());

            if (result.Success)
                return Ok();
            else
                return Content(HttpStatusCode.InternalServerError, result.Errors);
        }
    }
}
