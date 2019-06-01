using System;
using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.RequestModels;

namespace Ikuzo.Controllers
{
    [RoutePrefix("v1/api/itineraries")]
    public class ItineraryController : ApiController
    {
        private readonly ILineApp _lineApp;

        public ItineraryController(ILineApp lineApp)
        {
            _lineApp = lineApp;
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
        [Route("route")]
        public IHttpActionResult GetRoute([FromUri] RouteRequest request)
        {
           /* if (request == null)
                return BadRequest(); 

            var line = _lineApp.GetLocalLines(request.Lat, request.Lon, request.Distance);

            if (line != null)
                return Ok(line);*/

            return NotFound();
        }
    }
}
