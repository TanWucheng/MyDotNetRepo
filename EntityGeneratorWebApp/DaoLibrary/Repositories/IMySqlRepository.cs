using System.Collections.Generic;
using System.Threading.Tasks;
using DaoLibrary.Entities;

namespace DaoLibrary.Repositories
{
    public interface IMySqlRepository
    {
        public Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName);

        public Task<IdentityUser> GetIdentityUserAsync(string userIdentity, string password);
    }
}
