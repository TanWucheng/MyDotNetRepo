using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using MyTimingWebAppDemo.Models;

namespace MyTimingWebAppDemo.DataAccessLayer.Repositories
{
    internal class PersonRepository : RepositoryBase, IPersonRepository
    {
        public PersonRepository(IOptions<ConnectionStrings> options) : base(options) { }

        public IEnumerable<PersonEntity> GetAll()
        {
            using var connection = new SqlConnection(ConnectionStrings.SqlServer);
            const string sql = "select id, name, sex, age, address, email, phonenum from Person;";
            return connection.Query<PersonEntity>(sql);
        }
    }
}