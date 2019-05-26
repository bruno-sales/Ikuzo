using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
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
            var request = new RestRequest("rest/index.cfm/onibus", Method.GET)
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
                DateTime.TryParseExact(gps[0], "MM-dd-yyyy HH:mm:ssZ", CultureInfo.InvariantCulture,
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
                    ModalId = gps[1],
                    LineId = gps[2].Replace(".0", ""),
                    Direction = direction,
                    Timestamp = timestamp,
                    Latitude = latitude,
                    Longitude = longitude
                };

                gpsList.Add(gpsItem);
            }

            return gpsList;
        }

        public IEnumerable<Itinerary> GetItineraryInformation(string lineId)
        {
            var itineraries = new List<Itinerary>();
            try
            {
                var urlPrefix = ConfigurationManager.AppSettings["DataRioUrl"];

                var url = urlPrefix + "/csv/gtfs/onibus/percursos/gtfs_linha" + lineId + "-shapes.csv";

                var req = (HttpWebRequest)WebRequest.Create(url);
                var resp = (HttpWebResponse)req.GetResponse();

                var sr = new StreamReader(resp.GetResponseStream());

                var results = sr.ReadToEnd();
                sr.Close();

                results = results.Replace("\"", "").Replace("\r", "");

                var tempStr = results.Split('\n');

                var previousSequence = -1;
                var isReturning = false;

                for (var i = 1; i < tempStr.Length; i++)
                {
                    var itineraryInfo = tempStr[i].Split(',');

                    //Parse Latitude
                    decimal.TryParse(itineraryInfo[5], NumberStyles.Any, new CultureInfo("en-US"), out var latitude);

                    //Parse Longitude
                    decimal.TryParse(itineraryInfo[6], NumberStyles.Any, new CultureInfo("en-US"), out var longitude);

                    //Parse Sequence
                    int.TryParse(itineraryInfo[3], out var sequence);
                    
                    //Check if it's a returning route
                    if (sequence < previousSequence)
                    {
                        isReturning = !isReturning;
                    }
                    previousSequence = sequence;

                    var itinerary = new Itinerary()
                    {
                        LineId = itineraryInfo[0],
                        Sequence = sequence,
                        Latitude = latitude,
                        Longitude = longitude,
                        Returning = isReturning
                    };

                    itineraries.Add(itinerary);

                }


                return itineraries;
            }
            catch (Exception ex)
            {
                return itineraries;
            }
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
