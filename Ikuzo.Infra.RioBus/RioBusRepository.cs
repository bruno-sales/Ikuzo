using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.CrossCuttings;
using Ikuzo.Domain.ValueObjects.RioBus;
using Newtonsoft.Json;
using RestSharp;

namespace Ikuzo.Infra.RioBus
{
    public class RioBusRepository : IRioBusRepository
    {
        private readonly IRestClient _client;

        public RioBusRepository()
        {
            _client = new RestClient(ConfigurationManager.AppSettings["RioBusUrl"]);
        }

        public IEnumerable<RbLine> GetAllLines()
        {
            var request = new RestRequest("itinerary", Method.GET)
            {
                JsonSerializer = new MySerializer()
            };

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new List<RbLine>();

            var lines = JsonConvert.DeserializeObject<List<RbLine>>(response.Content);

            return lines;
        }


        public IEnumerable<RbBus> GetBusesInfoFromLine(string externalId)
        {
            var request = new RestRequest("search/{externalId}", Method.GET)
            {
                JsonSerializer = new MySerializer()
            };

            request.AddUrlSegment("externalId", externalId);

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new List<RbBus>();

            var lines = JsonConvert.DeserializeObject<List<RbBus>>(response.Content);

            return lines;
        }

        public IEnumerable<Gps> GetGpsInfoFromLine(string lineExternalId)
        {
            var request = new RestRequest("search/{line}", Method.GET)
            {
                JsonSerializer = new MySerializer()
            };

            request.AddUrlSegment("line", lineExternalId);

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new List<Gps>();

            var gps = JsonConvert.DeserializeObject<List<Gps>>(response.Content);

            return gps;
        }

        public IEnumerable<Gps> GetGpsInfoFromBus(string busId)
        {
            var request = new RestRequest("search/{busId}", Method.GET)
            {
                JsonSerializer = new MySerializer()
            };

            request.AddUrlSegment("busId", busId);

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new List<Gps>();

            var gps = JsonConvert.DeserializeObject<List<Gps>>(response.Content);

            return gps;
        }

        public void Dispose()
        {

            GC.Collect();
        }
    }
}
