using System.Web.Http;
using Ikuzo.Application.Interfaces;

namespace Ikuzo.Controllers
{
    [RoutePrefix("v1/api/buses")]
    public class BusController : ApiController
    {
        private readonly IBusApp _busApp;

        public BusController(IBusApp busApp)
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
        [Route("{busId}")]
        public IHttpActionResult GetBus([FromUri] string busId)
        {
            var bus = _busApp.GetBus(busId);

            if(bus != null)
                return Ok(bus);

            return NotFound();
        }

    }
}
