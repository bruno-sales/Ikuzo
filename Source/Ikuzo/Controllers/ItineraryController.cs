using System;
using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.RequestModels;

namespace Ikuzo.Controllers
{
    [RoutePrefix("api/v1/itineraries")]
    public class ItineraryController : ApiController
    {
        private readonly IItineraryApp _itineraryApp;

        public ItineraryController(IItineraryApp itineraryApp)
        {
            _itineraryApp = itineraryApp;
        }
        
        [HttpGet]
        [Route("route")]
        public IHttpActionResult GetRoute([FromUri] RouteRequest request)
        {
            if (request == null)
                return BadRequest(); 

            var lines = _itineraryApp.GetLocalToDestinyLines(request.LatOri, request.LonOri, request.LatDest, request.LonDest, 300 );

            if (lines != null)
                return Ok(lines);

            return NotFound();
        }
    }
}
