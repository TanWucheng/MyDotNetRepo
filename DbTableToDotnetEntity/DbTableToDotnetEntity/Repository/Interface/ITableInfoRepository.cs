using System.Collections.Generic;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;

namespace DbTableToDotnetEntity.Repository.Interface
{
    public interface ITableInfoRepository
    {
        Task<IEnumerable<TableInfo>> GetTableInfoAsync(string schemaName);
    }
}
