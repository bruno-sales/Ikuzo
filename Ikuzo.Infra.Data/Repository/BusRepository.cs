using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(Context.Context context) : base(context)
        {
        }

        public override IEnumerable<Bus> GetAll()
        {
            return DbSet
                .Include(i => i.Line);
        } 

        public Bus Details(string busId)
        {
            return DbSet
                .Include(i => i.Line)
                .FirstOrDefault(i => string.Equals(i.BusId.ToLower(), busId.ToLower()));
        }

        public void RemoveFromLine(string lineId)
        {
            var itens = DbSet.Where(i => string.Equals(i.LineId.ToLower(), lineId.ToLower())).ToList();

            foreach (var obj in itens)
            {
                DbSet.Remove(obj);
            }
        }
    }
}
