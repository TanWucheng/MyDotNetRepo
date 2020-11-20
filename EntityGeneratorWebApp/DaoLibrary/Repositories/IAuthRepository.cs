using System.Threading.Tasks;
using DaoLibrary.Entities;

namespace DaoLibrary.Repositories
{
    public interface IAuthRepository
    {
        public Task<IdentityUser> FindUser(string userIdentity, string password, DatabaseType databaseType);
    }
}
