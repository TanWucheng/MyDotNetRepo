using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Data;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DbTableToDotnetEntity.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
    {
        private readonly EfCoreContext _efCoreContext;

        public RepositoryBase(EfCoreContext efCoreContext)
        {
            _efCoreContext = efCoreContext;
        }

        public virtual Task<T> GetById(int id)
        {
            return _efCoreContext.Set<T>().FindAsync(id).AsTask();
        }

        public virtual IEnumerable<T> List()
        {
            return _efCoreContext.Set<T>().AsEnumerable();
        }

        public virtual Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate)
        {
            return Task.Run(() => _efCoreContext.Set<T>().Where(predicate).AsEnumerable());
        }

        public virtual void Add(T entity)
        {
            _efCoreContext.Set<T>().Add(entity);
            _efCoreContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _efCoreContext.Set<T>().Remove(entity);
            _efCoreContext.SaveChanges();
        }

        public virtual void Edit(T entity)
        {
            _efCoreContext.Entry(entity).State = EntityState.Modified;
            _efCoreContext.SaveChanges();
        }
    }
}
