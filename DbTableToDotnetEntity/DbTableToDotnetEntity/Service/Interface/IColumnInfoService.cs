using System.Collections.Generic;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;

namespace DbTableToDotnetEntity.Service.Interface
{
    public interface IColumnInfoService
    {
        Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName);

        IEnumerable<ColumnInfo> GetColumnInfo(string schemaName, string tableName);
    }
}
