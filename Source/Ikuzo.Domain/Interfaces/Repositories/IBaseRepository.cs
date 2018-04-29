using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T Create(T myObj);
        T Get(int id);
        T Get(Guid id);
        T Edit(T myObj);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> exp); 
        IEnumerable<T> GetAll();
        void RemoveWhere(Expression<Func<T, bool>> exp);
    }
}
