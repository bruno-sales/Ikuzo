using System;
using System.Collections.Generic;
using System.Linq;
using Ikuzo.Application.Interfaces;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces;
using Ikuzo.Domain.Interfaces.CrossCuttings;
using Ikuzo.Domain.Interfaces.Services;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Application.App
{
    public class CrawlerApp : ICrawlerApp
    {
        private readonly IRioBusRepository _riobusRepository;
        private readonly ILineService _lineService;
        private readonly IUnitOfWork _work;

        public CrawlerApp(IRioBusRepository riobusRepository, ILineService lineService, IUnitOfWork work)
        {
            _riobusRepository = riobusRepository;
            _lineService = lineService;
            _work = work;
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

                //Parse Objects
                foreach (var rioBusLine in rioBusLines)
                {
                    var dbLine = _lineService.Details(rioBusLine.Line);

                    if (dbLine == null) //If does not exist, save
                    {
                        linesToCreate.Add(new Line(rioBusLine.Line, rioBusLine.Description));

                    }
                    else if (string.Equals(dbLine.Description.ToLower(), rioBusLine.Description.ToLower()) == false)
                    {//Check if Description changed
                        dbLine.Description = rioBusLine.Description;

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
                        _lineService.Edit(line);
                    }

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
