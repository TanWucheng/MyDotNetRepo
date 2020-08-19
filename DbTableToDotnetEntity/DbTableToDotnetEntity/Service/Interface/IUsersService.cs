using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;

namespace DbTableToDotnetEntity.Service.Interface
{
    public interface IUsersService
    {
        IEnumerable<Users> GeUserList();

        Task<Users> GetUser(int id);

        Task<IEnumerable<Users>> GetUserList(Expression<Func<Users, bool>> predicate);
    }
}
