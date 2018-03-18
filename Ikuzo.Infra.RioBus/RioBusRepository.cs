using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Net;
using Ikuzo.Domain.Interfaces.CrossCuttings;
using Ikuzo.Domain.ValueObjects;
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

        public IEnumerable<RioBusLine> GetAllLines()
        {
            var request = new RestRequest("itinerary", Method.GET)
            {
                JsonSerializer = new MySerializer()
            };

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new List<RioBusLine>();

            var lines = JsonConvert.DeserializeObject<List<RioBusLine>>(response.Content);

            return lines;

        }

        public void Dispose()
        {

            GC.Collect();
        }
    }
}
