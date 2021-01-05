using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.RequestModels;

namespace Ikuzo.Controllers
{
    
    [RoutePrefix("api/v1/lines")]
    public class LineController : ApiController
    {
        private readonly ILineApp _lineApp;

        public LineController(ILineApp lineApp)
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
        [Route("{lineId}")]
        public IHttpActionResult GetLine([FromUri] string lineId)
        {
            var line = _lineApp.GetLine(lineId);

            if (line != null)
                return Ok(line);

            return NotFound();
        }

        [HttpGet]
        [Route("{lineId}/itinerary")]
        public IHttpActionResult GetLineItinerary([FromUri] string lineId)
        {
            var line = _lineApp.GetLineItinerary(lineId);

            if (line != null)
                return Ok(line);

            return NotFound();
        }

        [HttpGet]
        [Route("local")]
        public IHttpActionResult LocalLines([FromUri] LocalLinesRequest request)
        {
            if (request == null)
                return BadRequest(); 

            var line = _lineApp.GetLocalLines(request.Lat, request.Lon, request.Distance);

            if (line != null)
                return Ok(line);

            return NotFound();
        }
    }
}
