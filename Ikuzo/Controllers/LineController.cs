using System.Web.Http;
using Ikuzo.Application.Interfaces;

namespace Ikuzo.Controllers
{
    [RoutePrefix("v1/api/buses")]
    public class LineController : ApiController
    {
        private readonly IBusApp _busApp;

        public LineController(IBusApp busApp)
        {
            _busApp = busApp;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBuses()
        {
            var buses = _busApp.GetBuses();

            return Ok(buses);
        }

        [HttpGet]
        [Route("{externalBusId}")]
        public IHttpActionResult GetBus([FromUri] string externalBusId)
        {
            var bus = _busApp.GetBus(externalBusId);

            if(bus != null)
                return Ok(bus);

            return NotFound();
        }

    }
}
