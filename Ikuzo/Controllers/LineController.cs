using System.Web.Http;
using Ikuzo.Application.Interfaces;

namespace Ikuzo.Controllers
{
    [RoutePrefix("v1/api/lines")]
    public class LineController : ApiController
    {
        private readonly ILineApp _lineApp;

        public LineController(ILineApp lineApp)
        {
            _lineApp = lineApp;
        }

        [HttpGet]
        public IHttpActionResult Get()
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
