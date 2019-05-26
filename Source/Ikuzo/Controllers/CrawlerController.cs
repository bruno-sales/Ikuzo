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

        /*  
           1.1- Disponibilizar cadastro de itinerarios  
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
        [Route("modals")]
        public IHttpActionResult SyncModals()
        {
            var result = new ValidationResult();

            result.AddError(_crawlerApp.SyncModals());

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
