using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreSwaggerUse.Data;
using AspNetCoreSwaggerUse.Models;
using AspNetCoreSwaggerUse.Services.Interfaces;

namespace AspNetCoreSwaggerUse.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(EfDbContext efDbContext) : base(efDbContext)
        {
        }

        public Task<int> DeleteUserAsync(int id)
        {
            return DeleteOneAsync(id);
        }

        public Task<IEnumerable<User>> GetAllUserAsync()
        {
            return GetAllAsync();
        }

        public Task<User> GetUserAsync(int id)
        {
            return GetOneAsync(id);
        }

        Task<int> IUserService.InsertOneAsync(User user)
        {
            return InsertOneAsync(user);
        }
    }
}