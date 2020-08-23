using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor.Models.Entity;
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
            var pwd = Encryption.Md5(Encryption.Md5(password));
            return GetOneAsync(PredicateBuilder.True<User>().And<User>(x => x.Name == name).And<User>(x => x.Password == pwd));
        }
    }
}
