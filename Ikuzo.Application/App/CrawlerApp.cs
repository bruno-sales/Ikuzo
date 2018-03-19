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
        private readonly IUnitOfWork _work;

        public CrawlerApp(IRioBusRepository riobusRepository, ILineService lineService, IUnitOfWork work, IBusService busService)
        {
            _riobusRepository = riobusRepository;
            _lineService = lineService;
            _work = work;
            _busService = busService;
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
                        var rioBusBuses = _riobusRepository.GetBusesInfoFromLine(line.ExternalId).ToList();

                        if (rioBusBuses.Any()) //Having buses
                        {
                            foreach (var rioBusBus in rioBusBuses)
                            {
                                //Add to Save
                                busesToCreate.Add(new Bus(line.LineId, rioBusBus.Order));
                            }
                        }
                    }

                    if (busesToCreate.Any() == false)
                        continue;

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
    }
}
