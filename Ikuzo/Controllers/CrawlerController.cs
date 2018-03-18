using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Ikuzo.Application.Interfaces;
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
        [Route("sync")]
        public IHttpActionResult Sync()
        {
            var result = _crawlerApp.SyncLines();

            if (result.Success)
                return Ok();
            else
                return Content(HttpStatusCode.InternalServerError, result.Errors);

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
