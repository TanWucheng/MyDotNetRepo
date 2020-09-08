using System.Collections.Generic;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Repository.Interface;
using DbTableToDotnetEntity.Service.Interface;

namespace DbTableToDotnetEntity.Service
{
    public class TableInfoService : ITableInfoService
    {
        private readonly ITableInfoRepository _repository;

        public TableInfoService(ITableInfoRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TableInfo>> GetTableInfoAsync(string schemaName)
        {
            return _repository.GetTableInfoAsync(schemaName);
        }
    }
}
