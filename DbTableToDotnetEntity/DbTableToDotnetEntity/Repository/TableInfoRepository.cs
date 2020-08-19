using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Data;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace DbTableToDotnetEntity.Repository
{
    public class TableInfoRepository : ITableInfoRepository
    {
        private readonly EfCoreContext _dbContext;

        public TableInfoRepository(EfCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<TableInfo>> GetTableInfoAsync(string schemaName)
        {
            var param = new MySqlParameter
            {
                DbType = DbType.String,
                ParameterName = "p_schema_name",
                Value = schemaName
            };

            return Task.Run(() =>
                _dbContext.Set<TableInfo>().FromSqlRaw("call sp_query_table_info(@p_schema_name)", param).AsNoTracking()
                    .AsEnumerable());
        }
    }
}