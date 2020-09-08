using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Repository.Interface;
using DbTableToDotnetEntity.Service.Interface;

namespace DbTableToDotnetEntity.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Users> GeUserList()
        {
            return _repository.List();
        }

        public async Task<Users> GetUser(int id)
        {
            var user = await _repository.GetById(id);
            return user;
        }

        public Task<IEnumerable<Users>> GetUserList(Expression<Func<Users, bool>> predicate)
        {
            return _repository.List(predicate);
        }
    }
}
