using System.Collections.Generic;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Repository.Interface;
using DbTableToDotnetEntity.Service.Interface;

namespace DbTableToDotnetEntity.Service
{
    public class ColumnInfoService : IColumnInfoService
    {
        private readonly IColumnInfoRepository _repository;

        public ColumnInfoService(IColumnInfoRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName)
        {
            return _repository.GetColumnInfoAsync(schemaName, tableName);
        }

        public IEnumerable<ColumnInfo> GetColumnInfo(string schemaName, string tableName)
        {
            return _repository.GetColumnInfo(schemaName, tableName);
        }
    }
}
