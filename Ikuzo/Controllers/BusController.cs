﻿using System;
using System.Web.Http;
using Ikuzo.Application.Interfaces;
using Ikuzo.RequestModels;

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

        [HttpGet]
        [Route("nearby")]
        public IHttpActionResult NearbyBuses([FromUri] NerbyBusesRequest request)
        {
            if (request == null)
                return BadRequest();

            decimal variance;

            try
            {
                if (request.Var == null)
                    variance = new decimal(0.010000);
                else
                    variance = Convert.ToDecimal(request.Var) / (decimal)1000000.0;
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var line = _busApp.GetNearbyBuses(request.Lat, request.Lon, variance, request.Line);

            if (line != null)
                return Ok(line);

            return NotFound();
        }
    }
}
