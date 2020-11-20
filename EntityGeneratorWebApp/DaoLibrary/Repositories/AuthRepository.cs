using System.Threading.Tasks;
using DaoLibrary.Entities;

namespace DaoLibrary.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMySqlRepository _mySqlRepository;

        public AuthRepository(IMySqlRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        public Task<IdentityUser> FindUser(string userIdentity, string password, DatabaseType databaseType)
        {
            return databaseType switch
            {
                DatabaseType.MySql => _mySqlRepository.GetIdentityUserAsync(userIdentity, password),
                _ => null
            };
        }
    }
}
