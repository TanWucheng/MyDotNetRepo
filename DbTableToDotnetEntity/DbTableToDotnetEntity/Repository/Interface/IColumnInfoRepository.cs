using System.Collections.Generic;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;

namespace DbTableToDotnetEntity.Repository.Interface
{
    public interface IColumnInfoRepository
    {
        Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName);

        IEnumerable<ColumnInfo> GetColumnInfo(string schemaName, string tableName);
    }
}
