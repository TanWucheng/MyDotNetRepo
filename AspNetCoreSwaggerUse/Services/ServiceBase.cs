using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCoreSwaggerUse.Data;
using AspNetCoreSwaggerUse.Domain;
using AspNetCoreSwaggerUse.Models;

namespace AspNetCoreSwaggerUse.Services
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

        protected virtual Task<int> InsertOneAsync(User user)
        {
            return Task.Run(() =>
            {
                _efDbContext.Users.Add(user);
                return _efDbContext.SaveChanges();
            });
        }

        protected virtual Task<int> DeleteOneAsync(int id)
        {
            return Task.Run(async () =>
            {
                var user = await GetOneAsync(id);
                _efDbContext.Remove(user);
                return _efDbContext.SaveChanges();
            });
        }
    }
}