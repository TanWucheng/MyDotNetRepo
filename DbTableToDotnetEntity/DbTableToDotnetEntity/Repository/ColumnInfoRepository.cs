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
    public class ColumnInfoRepository : IColumnInfoRepository
    {
        private readonly EfCoreContext _dbContext;

        public ColumnInfoRepository(EfCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName)
        {
            object[] parameters = {
                new MySqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "p_schema_name",
                    Value = schemaName
                },
                new MySqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "p_table_name",
                    Value = tableName
                }
            };

            return Task.Run(() =>
            {
                var result = _dbContext.Set<ColumnInfo>()
                    .FromSqlRaw("call sp_query_column_info(@p_schema_name,@p_table_name)", parameters).AsNoTracking();
                return result.AsEnumerable();
            });
        }

        public IEnumerable<ColumnInfo> GetColumnInfo(string schemaName, string tableName)
        {
            object[] parameters = {
                new MySqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "p_schema_name",
                    Value = schemaName
                },
                new MySqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "p_table_name",
                    Value = tableName
                }
            };

            return _dbContext.Set<ColumnInfo>()
                  .FromSqlRaw("call sp_query_column_info(@p_schema_name,@p_table_name)", parameters)
                  .AsEnumerable();
        }
    }
}
