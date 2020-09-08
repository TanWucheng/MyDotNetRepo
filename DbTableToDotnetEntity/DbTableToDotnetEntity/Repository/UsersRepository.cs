using DbTableToDotnetEntity.Data;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Repository.Interface;

namespace DbTableToDotnetEntity.Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        private readonly EfCoreContext _dbContext;

        public UsersRepository(EfCoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
