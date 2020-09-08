using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;

namespace DbTableToDotnetEntity.Repository.Interface
{
    public interface IRepositoryBase<T> where T : BaseModel
    {
        Task<T> GetById(int id);

        IEnumerable<T> List();

        Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);

        void Edit(T entity);
    }
}
