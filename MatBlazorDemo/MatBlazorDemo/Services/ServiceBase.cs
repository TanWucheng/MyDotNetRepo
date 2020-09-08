using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MatBlazor.Model.Entity;
using MatBlazorDemo.Data;
using MatBlazorDemo.Domain;

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
    }
}
