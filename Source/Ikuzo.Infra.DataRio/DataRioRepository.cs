using System;
using Ikuzo.Domain.Interfaces.CrossCuttings;

namespace Ikuzo.Infra.DataRio
{
    public class DataRioRepository : IDataRioRepository
    {
        public void Dispose()
        {

            GC.Collect();
        }
    }
}
