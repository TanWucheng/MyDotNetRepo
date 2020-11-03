using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BasicUtilsLibrary.Encryption;
using DaoLibrary.Entities;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DaoLibrary.Repositories
{
    public class MySqlRepository : RepositoryBase, IMySqlRepository
    {
        public MySqlRepository(IOptions<ConnectionStrings> options) : base(options) { }

        public Task<IEnumerable<ColumnInfo>> GetColumnInfoAsync(string schemaName, string tableName)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(ConnectionStrings.MySql);
                var parameters = new DynamicParameters();
                parameters.Add("p_schema_name", schemaName, DbType.String);
                parameters.Add("p_table_name", tableName, DbType.String);
                var result = connection.Query<ColumnInfo>("sp_query_column_info", parameters, commandType: CommandType.StoredProcedure);
                return result;
            });
        }

        public Task<IdentityUser> GetIdentityUserAsync(string userIdentity, string password)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(ConnectionStrings.MySql);
                var parameters = new DynamicParameters();
                password = Md5.Md5Encrypt(password);
                password = Md5.Md5Encrypt(password);
                parameters.Add("p_identity", userIdentity, DbType.String);
                parameters.Add("p_password", password, DbType.String);
                var result = connection.Query<IdentityUser>("sp_query_identity_user", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            });
        }
    }
}
