using System;
using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.RequestModels;

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
        [Route("local")]
        public IHttpActionResult LocalLines([FromUri] LocalLinesRequest request)
        {
            if(request == null)
                return BadRequest();

            decimal variance;

            try
            {
                if (request.Var == null)
                    variance = new decimal(0.001000);
                else
                    variance = Convert.ToDecimal(request.Var) / (decimal)1000000.0;
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var line = _lineApp.GetLocalLines(request.Lat,request.Lon,variance);

            if (line != null)
                return Ok(line);

            return NotFound();
        }
    }
}
