using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

    }
}
