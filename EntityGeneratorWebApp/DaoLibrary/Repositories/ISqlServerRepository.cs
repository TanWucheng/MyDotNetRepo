using System.Collections.Generic;
using System.Threading.Tasks;
using DaoLibrary.Entities;

namespace DaoLibrary.Repositories
{
    public interface ISqlServerRepository
    {
        public Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName);
    }
}
