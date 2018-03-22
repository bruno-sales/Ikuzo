using System.Web.Http;
using Ikuzo.Application.Interfaces;

namespace Ikuzo.Controllers
{
    [RoutePrefix("v1/api/lines")]
    public class BusController : ApiController
    {
        private readonly ILineApp _lineApp;

        public BusController(ILineApp lineApp)
        {
            _lineApp = lineApp;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetLines()
        {
            var lines = _lineApp.GetLines();

            return Ok(lines);
        }

        [HttpGet]
        [Route("{externalLineId}")]
        public IHttpActionResult GetLine([FromUri] string externalLineId)
        {
            var line = _lineApp.GetLine(externalLineId);

            if(line != null)
                return Ok(line);

            return NotFound();
        }

    }
}
