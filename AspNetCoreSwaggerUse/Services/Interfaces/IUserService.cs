using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreSwaggerUse.Models;

namespace AspNetCoreSwaggerUse.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);

        Task<int> InsertOneAsync(User user);

        Task<IEnumerable<User>> GetAllUserAsync();

        Task<int> DeleteUserAsync(int id);
    }
}