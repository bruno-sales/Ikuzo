using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ExpressMapper;
using Ikuzo.Application.Interfaces;
using Ikuzo.Application.ViewModels.Modal;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Helpers;
using Ikuzo.Domain.Interfaces.Services;

namespace Ikuzo.Application.App
{
    public class ModalApp : IModalApp
    {
        private readonly IModalService _modalService;
        private readonly IGpsService _gpsService;

        public ModalApp(IModalService modalService, IGpsService gpsService)
        {
            _modalService = modalService;
            _gpsService = gpsService;
        }

        public IEnumerable<ModalIndex> GetModals()
        {
            var modals = _modalService.GetAllModals().ToList();

            var modalsIndex = Mapper.Map<List<Modal>, List<ModalIndex>>(modals);

            return modalsIndex;
        }

        public ModalDetails GetModal(string modalId)
        {
            var modal = _modalService.Details(modalId);

            if (modal == null)
                return null;

            var modalDetail = Mapper.Map<Modal, ModalDetails>(modal);
 
            var gps = _gpsService.GetModalGps(modalId);

            modalDetail.Gps = new ModalGps()
            {
                Latitude = gps.Latitude,
                Longitude = gps.Longitude,
                Direction = gps.Direction.ToString()
            };

            return modalDetail;
        }

        public IEnumerable<ModalNearbyDetails> GetNearbyModals(decimal latitude, decimal longitude, decimal? distance, string lineId)
        {
            var modals = new List<ModalNearbyDetails>();

            if (distance == null)
                distance = Convert.ToDecimal(ConfigurationManager.AppSettings["DefaultDistance"] ?? "200");

            var gpses = _gpsService.GetNerbyModalsGps(latitude, longitude, distance.Value, lineId).ToList();

            if (gpses.Any())
            {
                var recentGpses = gpses.Where(i => i.Timestamp.Year == DateTime.UtcNow.Year &&
                                                   i.Timestamp.Month == DateTime.UtcNow.Month &&
                                                   i.Timestamp.Day == DateTime.UtcNow.Day &&
                                                   i.Timestamp.Hour == DateTime.UtcNow.Hour &&
                                                   i.Timestamp.Minute - DateTime.UtcNow.Minute < 5).ToList();

                foreach (var gps in recentGpses)
                {
                    var distanceInMeters =
                        GpsHelper.DistanceBetweenCoordenates(latitude, longitude, gps.Latitude, gps.Longitude);
                    var modalAvgSpeed = Convert.ToDouble(ConfigurationManager.AppSettings["BusAvgSpeed"] ?? "4.2");

                    var minutesToArrive = (distanceInMeters / modalAvgSpeed) / 60.0;
                    var modal = new ModalNearbyDetails()
                    {
                        Modal = gps.ModalId,
                        Line = gps.LineId,
                        Distance = distanceInMeters,
                        //MinutesToArrive = Convert.ToInt32(minutesToArrive),
                        LastUpdateDate = gps.LastUpdateDate,
                        TimeStamp = gps.Timestamp,
                        Gps = new ModalGps()
                        {
                            Latitude = gps.Latitude,
                            Longitude = gps.Longitude,
                            Direction = gps.Direction.ToString()
                        }
                    };

                    modals.Add(modal);
                }
            }

            return modals.OrderBy(i=>i.Distance);
        }
    }
}
