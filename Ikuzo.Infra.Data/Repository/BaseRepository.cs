using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions; 
using Ikuzo.Domain.Interfaces.Repositories;

namespace Ikuzo.Infra.Data.Repository
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        protected readonly Context.Context Db;
        protected readonly IDbSet<T> DbSet;

        public BaseRepository(Context.Context context)
        {
            Db = context;
            DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetWhere(Expression<Func<T, bool>> exp)
        {
            return DbSet.Where(exp).ToList();
        }

        public virtual void RemoveWhere(Expression<Func<T, bool>> exp)
        {
            foreach (var obj in DbSet.Where(exp).ToList())
            {
                DbSet.Remove(obj);
            }
        }

        public T Create(T myObj)
        {
            var obj = DbSet.Add(myObj);

            return obj;

        }

        public virtual IEnumerable<T> Create(IEnumerable<T> myObj)
        {
            var item = myObj.ToList();

            foreach (var obj in item)
            {
                DbSet.Add(obj);
            }

            return item;
        }

        public virtual T Get(int id)
        {
            return DbSet.Find(id);
        }
        
        public virtual T Get(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Edit(T myObj)
        {
            if (!IsAttached(myObj))
            {
                var obj = DbSet.Attach(myObj);
                myObj = obj;
            }

            Db.Entry(myObj).State = EntityState.Modified;

            return myObj;
        }

        private bool IsAttached(T obj)
        {
            if (obj == null)
            {
                return false;
            }

            var manager = ((IObjectContextAdapter)Db).ObjectContext.ObjectStateManager;

            if (manager.TryGetObjectStateEntry(obj, out var entry))
            {
                return entry.State != EntityState.Detached;
            }
            return false;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
