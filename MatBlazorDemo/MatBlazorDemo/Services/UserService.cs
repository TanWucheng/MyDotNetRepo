using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor.Model.Entity;
using MatBlazorDemo.Data;
using MatBlazorDemo.Domain;
using MatBlazorDemo.Services.Interfaces;

namespace MatBlazorDemo.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(EfDbContext efDbContext) : base(efDbContext)
        {
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return GetAllAsync();
        }

        public Task<User> GetUserAsync(string name, string password)
        {
            // var pwd = Encryption.Md5(Encryption.Md5(password));
            return GetOneAsync(PredicateBuilder.True<User>().And(x => x.Name == name).And(x => x.Password == password));
        }

        public Task<User> GetUserAsync(int id)
        {
            return GetOneAsync(id);
        }

        public Task<IEnumerable<User>> PaginationSelectUserAsync(int pageIndex, int pageSize)
        {
            return PaginationSelectAsync(pageIndex, pageSize);
        }

        public Task<int> GetUserTotalCount()
        {
            return GetTotalCountAsync();
        }

        public Task<int> UpdateUserAsync(User user)
        {
            return UpdateOneAsync(user);
        }

        public Task<int> InsertUserAsync(User user)
        {
            return InsertOneAsync(user);
        }

        public Task<int> DeleteUserAsync(int id)
        {
            return DeleteOneAsync(id);
        }

        public Task<int> DeleteUsersAsync(IEnumerable<int> idCollection)
        {
            return DeleteRangeAsync(idCollection);
        }
    }
}
