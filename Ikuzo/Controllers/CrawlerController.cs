using System.Collections.Generic;
using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.Filter;

namespace Ikuzo.Controllers
{
    [AuthorizationRequest]
    [RoutePrefix("v1/api/Crawler")]
    public class CrawlerController : ApiController
    {
        private readonly ICrawlerApp _crawlerApp;

        public CrawlerController(ICrawlerApp crawlerApp)
        {
            _crawlerApp = crawlerApp;
        }
        
        [HttpPost]
        [Route("Sync")]
        public IHttpActionResult Sync()
        {
            var result = _crawlerApp.SyncLines();

            if(result.Success)
                return Ok();
            else
                return InternalServerError();
        
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
