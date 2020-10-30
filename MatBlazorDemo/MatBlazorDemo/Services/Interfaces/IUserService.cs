using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor.Model.Entity;

namespace MatBlazorDemo.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserAsync(string name, string password);

        Task<User> GetUserAsync(int id);

        Task<IEnumerable<User>> PaginationSelectUserAsync(int pageIndex, int pageSize);

        Task<int> GetUserTotalCount();

        Task<int> UpdateUserAsync(User user);

        Task<int> InsertUserAsync(User user);

        Task<int> DeleteUserAsync(int id);

        Task<int> DeleteUsersAsync(IEnumerable<int> idCollection);
    }
}
