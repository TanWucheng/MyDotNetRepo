using System.Collections.Generic;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;

namespace DbTableToDotnetEntity.Service.Interface
{
    public interface ITableInfoService
    {
        Task<IEnumerable<TableInfo>> GetTableInfoAsync(string schemaName);
    }
}
