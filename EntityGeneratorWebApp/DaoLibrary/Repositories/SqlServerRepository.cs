using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DaoLibrary.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DaoLibrary.Repositories
{
    public class SqlServerRepository : RepositoryBase, ISqlServerRepository
    {
        public SqlServerRepository(IOptions<ConnectionStrings> options) : base(options) { }

        public Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName)
        {
            return Task.Run(() =>
            {
                using var connection = new SqlConnection(ConnectionStrings.SqlServer);
                var parameters = new DynamicParameters();
                parameters.Add("@p_schema_name", schemaName, DbType.String);
                parameters.Add("@p_table_name", tableName, DbType.String);
                var result = connection.Query<ColumnInfo>("sp_query_column_info", parameters, commandType: CommandType.StoredProcedure);
                return result;
            });
        }
    }
}
