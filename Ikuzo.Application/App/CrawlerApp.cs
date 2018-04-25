using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Application.Interfaces;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.CrossCuttings;
using Ikuzo.Domain.Interfaces.Services;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Application.App
{
    public class CrawlerApp : ICrawlerApp
    {
        private readonly IRioBusRepository _riobusRepository;
        private readonly ILineService _lineService;
        private readonly IBusService _busService;
        private readonly IItineraryService _itineraryService;
        private readonly IGpsService _gpsService;
        private readonly IUnitOfWork _work;

        public CrawlerApp(IRioBusRepository riobusRepository, ILineService lineService, IUnitOfWork work, IBusService busService, IGpsService gpsService, IItineraryService itineraryService)
        {
            _riobusRepository = riobusRepository;
            _lineService = lineService;
            _work = work;
            _busService = busService;
            _gpsService = gpsService;
            _itineraryService = itineraryService;
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
                    var dbLine = _lineService.Details(rioBusLine.Line);

                    if (dbLine == null) //If does not exist
                    {
                        //Add to Save
                        linesToCreate.Add(new Line(rioBusLine.Line, rioBusLine.Description));

                    }
                    else if (string.Equals(dbLine.Description.ToLower(), rioBusLine.Description.ToLower()) == false)
                    {   //Check if Description changed

                        dbLine.Description = rioBusLine.Description;
                        dbLine.LastUpdateDate = DateTime.Now;

                        //Add to update
                        linesToUpdate.Add(dbLine);
                    }
                }

                if (linesToCreate.Any())
                {
                    //Create
                    _lineService.CreateLines(linesToCreate);

                    //Commit
                    validation.AddError(_work.Commit());
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

        public ValidationResult SyncBuses()
        {
            var validation = new ValidationResult();

            try
            {
                //Get data from external resource
                var rioBusLines = _riobusRepository.GetAllLines().ToList();

                //Analyse Objects
                foreach (var rioBusLine in rioBusLines)
                {
                    var busesToCreate = new List<Bus>();

                    //Get line from database
                    var line = _lineService.Details(rioBusLine.Line);

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

                                busesToCreate.Add(new Bus(rioBusBus.Order, line.LineId)); //Add to Save
                            }
                        }
                    }

                    if (busesToCreate.Any() == false)
                        continue;

                    //Remove previous bus info
                    validation.AddError(_busService.RemoveBusesFromLine(line.LineId));

                    //Create
                    _busService.CreateBuses(busesToCreate);

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

        public ValidationResult SyncItineraries()
        {
            var validation = new ValidationResult();

            try
            {
                //Get lines
                var lines = _lineService.GetAllLines().ToList();

                //Analyse Objects
                foreach (var line in lines)
                {
                    var itinerariesToCreate = new List<Itinerary>();

                    //Get gps info from external resource
                    var rioBusItineraries = _riobusRepository.GetItineraryInfoFromLine(line.LineId);

                    if (rioBusItineraries.Spots.Any()) //Having infos
                    {
                        foreach (var spot in rioBusItineraries.Spots)
                        {
                            var itinerary = new Itinerary(line.LineId, spot.Latitude, spot.Longitude, spot.Returning);
                            itinerariesToCreate.Add(itinerary); //Add to Save
                        }
                    }

                    if (itinerariesToCreate.Any() == false)
                        continue;

                    //Remove previous gps info
                    validation.AddError(_itineraryService.RemoveItinerariesFromLine(line.LineId));

                    //Create
                    _itineraryService.CreateItineraries(itinerariesToCreate);

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


        public ValidationResult SyncGps()
        {
            var validation = new ValidationResult();

            try
            {
                //Get lines
                var lines = _lineService.GetAllLines().Select(i => i.LineId).ToList();

                var allGps = _riobusRepository.GetGpsInfoFromLines(lines).ToList();

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
