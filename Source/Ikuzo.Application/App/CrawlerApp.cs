using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Application.Interfaces;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Helpers;
using Ikuzo.Domain.Interfaces.CrossCuttings;
using Ikuzo.Domain.Interfaces.Services;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Application.App
{
    public class CrawlerApp : ICrawlerApp
    {
        private readonly IRioBusRepository _riobusRepository;
        private readonly IDataRioRepository _datarioRepository;
        private readonly ILineService _lineService;
        private readonly IModalService _modalService;
        private readonly IItineraryService _itineraryService;
        private readonly IGpsService _gpsService;
        private readonly IUnitOfWork _work;

        public CrawlerApp(IRioBusRepository riobusRepository, ILineService lineService, IUnitOfWork work, IModalService modalService, IGpsService gpsService, IItineraryService itineraryService, IDataRioRepository datarioRepository)
        {
            _riobusRepository = riobusRepository;
            _lineService = lineService;
            _work = work;
            _modalService = modalService;
            _gpsService = gpsService;
            _itineraryService = itineraryService;
            _datarioRepository = datarioRepository;
        }

        public ValidationResult SyncLines()
        {
            var validation = new ValidationResult();
            var linesToCreate = new List<Line>();
            var linesToUpdate = new List<Line>();

            try
            {
                //Get data from external resource
                var rioBusLines = _riobusRepository.GetAllLines().ToList();

                //Analyse Objects
                foreach (var rioBusLine in rioBusLines)
                {
                    //Get line from database
                    var dbLine = _lineService.Get(rioBusLine.Line);

                    if (dbLine == null) //If does not exist
                    {
                        //Add to Save
                        linesToCreate.Add(new Line(rioBusLine.Line, rioBusLine.Description));

                    }
                    else if (string.Equals(dbLine.Description.ToLower(), rioBusLine.Description.ToLower()) == false)
                    {   //Check if Description changed

                        dbLine.Description = rioBusLine.Description;
                        dbLine.LastUpdateDate = DateTime.UtcNow;

                        //Add to update
                        linesToUpdate.Add(dbLine);
                    }
                }

                if (linesToCreate.Any())
                {
                    //Create
                    _lineService.CreateLines(linesToCreate);
                }

                if (linesToUpdate.Any())
                {
                    foreach (var line in linesToUpdate)
                    {
                        //Update
                        _lineService.Edit(line);
                    }

                    //Commit
                    validation.AddError(_work.Commit());
                }

            }
            catch (Exception e)
            {
                validation.AddError(new ValidationError(e.Message));
            }

            return validation;
        }

        public ValidationResult SyncModals()
        {
            var validation = new ValidationResult();

            try
            {
                //Get data from external resource
                var rioBusLines = _riobusRepository.GetAllLines().ToList();

                //Analyse Objects
                foreach (var rioBusLine in rioBusLines)
                {
                    var modalsToCreate = new List<Modal>();

                    //Get line from database
                    var line = _lineService.Get(rioBusLine.Line);

                    if (line != null)
                    {
                        //Get buses from external resource
                        var rioBusBuses = _riobusRepository.GetBusesInfoFromLine(line.LineId).ToList();

                        if (rioBusBuses.Any()) //Having buses
                        {
                            foreach (var rioBusBus in rioBusBuses)
                            {
                                //Check if adding in the right line
                                if (!string.Equals(line.LineId.ToLower(), rioBusBus.Line.ToLower()))
                                    continue;

                                //if not already added
                                if (_modalService.Get(rioBusBus.Order) == null)
                                {
                                    modalsToCreate.Add(new Modal(rioBusBus.Order, line.LineId)); //Add to Save
                                }
                            }
                        }
                    }

                    if (modalsToCreate.Any() == false || line == null)
                        continue;

                    //Remove previous bus info
                    validation.AddError(_modalService.RemoveModalsFromLine(line.LineId));

                    //Create
                    _modalService.CreateModals(modalsToCreate);
                }
            }
            catch (Exception e)
            {
                validation.AddError(new ValidationError(e.Message));
            }

            return validation;
        }

        public ValidationResult SyncItineraries()
        {
            var validation = new ValidationResult();

            try
            {
                //Get lines
                var lines = _lineService.GetAllLines().ToList();

                var itinerariesToCreate = new List<Itinerary>();

                //Analyse Objects
                foreach (var line in lines)
                {
                    var itineraries = _datarioRepository.GetItineraryInformation(line.LineId).ToList();

                    var calculatedItineraries = new List<Itinerary>();

                    var goingItineraries = itineraries.Where(i => i.Returning == false).OrderBy(i => i.Sequence).ToList();
                    var returningItineraries = itineraries.Where(i => i.Returning).OrderBy(i => i.Sequence).ToList();

                    for (var i = 0; i < goingItineraries.Count - 1; i++)
                    {
                        var thisItinerary = goingItineraries[i];
                        var nextItinerary = goingItineraries[i + 1];

                        var distanceBetween = GpsHelper.DistanceBetweenCoordenates(thisItinerary.Latitude,
                            thisItinerary.Longitude, nextItinerary.Latitude, nextItinerary.Longitude);

                        thisItinerary.DistanceToNext = distanceBetween;

                        calculatedItineraries.Add(thisItinerary);
                    }

                    for (var i = 0; i < returningItineraries.Count - 1; i++)
                    {
                        var thisItinerary = returningItineraries[i];
                        var nextItinerary = returningItineraries[i + 1];

                        var distanceBetween = GpsHelper.DistanceBetweenCoordenates(thisItinerary.Latitude,
                            thisItinerary.Longitude, nextItinerary.Latitude, nextItinerary.Longitude);

                        thisItinerary.DistanceToNext = distanceBetween;

                        calculatedItineraries.Add(thisItinerary);
                    }

                    itinerariesToCreate.AddRange(calculatedItineraries);
                }

                if (itinerariesToCreate.Any())
                {
                    _itineraryService.RemoveAllItineraries();
                    _itineraryService.CreateItineraries(itinerariesToCreate);
                }
            }
            catch (Exception e)
            {
                validation.AddError(new ValidationError(e.Message));
            }

            return validation;
        }

        public ValidationResult SyncGps()
        {
            var validation = new ValidationResult();

            try
            {
                //Get lines
                //var lines = _lineService.GetAllLines().Select(i => i.LineId).ToList();

                //var allGps = _riobusRepository.GetGpsInfoFromLines(lines).ToList();

                var allGps = _datarioRepository.GetGpsInformation();

                //Remove previous gps info
                _gpsService.RemoveAllGpses();

                //Create
                _gpsService.CreateGpses(allGps);

            }
            catch (Exception e)
            {
                validation.AddError(new ValidationError(e.Message));
            }

            return validation;
        }

    }
}
