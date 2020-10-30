using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MatBlazor.Model.Entity;
using MatBlazorDemo.Data;
using MatBlazorDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace MatBlazorDemo.Services
{
    public class ServiceBase<T> where T : EntityBase
    {
        private readonly EfDbContext _efDbContext;

        public ServiceBase(EfDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        protected virtual Task<T> GetOneAsync(int id)
        {
            return Task.Run(() => _efDbContext.Set<T>().FirstOrDefault(PredicateBuilder.True<T>().And(x => x.Id == id)));
        }

        protected virtual Task<T> GetOneAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.Run(() => _efDbContext.Set<T>().FirstOrDefault(predicate));
        }

        protected virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.Run(() => _efDbContext.Set<T>().AsEnumerable());
        }

        protected virtual Task<IEnumerable<T>> PaginationSelectAsync(int pageIndex, int pageSize)
        {
            return Task.Run(() => _efDbContext.Set<T>().Skip(pageSize * (pageIndex)).Take(pageSize).AsEnumerable());
        }

        protected virtual Task<IEnumerable<T>> GetCollectionAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.Run(() => _efDbContext.Set<T>().Where(predicate).AsEnumerable());
        }

        protected virtual Task<int> GetTotalCountAsync()
        {
            return Task.Run(() => _efDbContext.Set<T>().Count());
        }

        protected virtual Task<int> UpdateOneAsync(T obj)
        {
            return Task.Run(() =>
            {
                _efDbContext.Update(obj);
                return _efDbContext.SaveChanges();
            });
        }

        protected virtual Task<int> InsertOneAsync(T obj)
        {
            return Task.Run(() =>
            {
                _efDbContext.Set<T>().Add(obj);
                return _efDbContext.SaveChanges();
            });
        }

        protected virtual Task<int> DeleteOneAsync(int id)
        {
            return Task.Run(async () =>
            {
                var obj = await GetOneAsync(id);
                _efDbContext.Set<T>().Remove(obj);
                return await _efDbContext.SaveChangesAsync();
            });
        }

        protected virtual Task<int> DeleteRangeAsync(IEnumerable<int> idCollection)
        {
            return Task.Run(async () =>
            {
                var collection = from entity in _efDbContext.Set<T>()
                                 where idCollection.Contains(entity.Id)
                                 select entity;
                _efDbContext.Set<T>().RemoveRange(collection);
                return await _efDbContext.SaveChangesAsync();
            });
        }
    }
}
