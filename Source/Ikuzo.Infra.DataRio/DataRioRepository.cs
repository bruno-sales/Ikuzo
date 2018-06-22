using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net; 
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.CrossCuttings; 
using Newtonsoft.Json;
using RestSharp;

namespace Ikuzo.Infra.DataRio
{
    public class DataRioRepository : IDataRioRepository
    {
        private readonly IRestClient _client;

        public DataRioRepository()
        {
            _client = new RestClient(ConfigurationManager.AppSettings["DataRioUrl"]);
        }
        

        public IEnumerable<Gps> GetGpsInformation()
        { 
            var request = new RestRequest("onibus", Method.GET)
            {
                JsonSerializer = new MySerializer()
            }; 

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new List<Gps>();

            var gpsDataRio = JsonConvert.DeserializeObject<DataRio>(response.Content);

            var gpsList = new List<Gps>();

            foreach (var gps in gpsDataRio.Data)
            { 

                //Parse timestamp
                DateTime.TryParseExact(gps[0], "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.AdjustToUniversal, out var timestamp);

                //Check if it is a valid datetime 
                timestamp = timestamp < new DateTime(2000, 1, 1) ? 
                                            DateTime.UtcNow.AddDays(-1) : 
                                            timestamp;

                //Parse Latitude
                decimal.TryParse(gps[3], NumberStyles.Any, new CultureInfo("en-US"), out var latitude);

                //Parse Longitude
                decimal.TryParse(gps[4], NumberStyles.Any, new CultureInfo("en-US"), out var longitude);

                //Parse Direciton
                int.TryParse(gps[6], out var direction);

                //build gps item
                var gpsItem = new Gps
                {
                    BusId = gps[1],
                    LineId = gps[2].Replace(".0",""),
                    Direction = direction,
                    Timestamp = timestamp,
                    Latitude = latitude,
                    Longitude = longitude
                };

                gpsList.Add(gpsItem);
            }

            return gpsList;
        }
         

        public void Dispose()
        {
            GC.Collect();
        }

        private class DataRio
        {
            public List<string> Columns { get; set; }
            public List<List<string>> Data { get; set; }
        }
    }
}
