using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ikuzo.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Create(T myObj);
        T Details(int id);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> exp);
        IEnumerable<T> Create(IEnumerable<T> myObj);
        IEnumerable<T> GetAll();
        void RemoveWhere(Expression<Func<T, bool>> exp);
    }
}
