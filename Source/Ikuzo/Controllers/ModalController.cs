using System;
using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.RequestModels;

namespace Ikuzo.Controllers
{
    [RoutePrefix("v1/api/modals")]
    public class ModalController : ApiController
    {
        private readonly IModalApp _modalApp;

        public ModalController(IModalApp modalApp)
        {
            _modalApp = modalApp;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetModals()
        {
            var modals = _modalApp.GetModals();

            return Ok(modals);
        }

        [HttpGet]
        [Route("{modalId}")]
        public IHttpActionResult GetModal([FromUri] string modalId)
        {
            var modal = _modalApp.GetModal(modalId);

            if(modal != null)
                return Ok(modal);

            return NotFound();
        }

        [HttpGet]
        [Route("nearby")]
        public IHttpActionResult NearbyModals([FromUri] NerbyModalsRequest request)
        {
            if (request == null)
                return BadRequest();

            decimal variance;

            try
            {
                if (request.Precision == null || request.Precision > 100 || request.Precision < 0)
                    variance = new decimal(0.0035); //0.0035 1060 m -> 0.01 3027 m
                else
                {
                    //a*X + b
                    variance = (request.Precision.Value * new decimal(-0.65) + new decimal(100)) / (decimal) 10000.0;
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var modals = _modalApp.GetNearbyModals(request.Lat, request.Lon, variance, request.Line);

            if (modals != null)
                return Ok(modals);

            return NotFound();
        }
    }
}
