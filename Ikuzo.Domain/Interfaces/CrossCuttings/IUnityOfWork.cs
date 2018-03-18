using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikuzo.Domain.ValueObjects;

namespace Ikuzo.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ValidationResult Commit();
    }
}
