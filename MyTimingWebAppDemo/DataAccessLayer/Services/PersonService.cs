using System.Collections.Generic;
using MyTimingWebAppDemo.DataAccessLayer.Repositories;
using MyTimingWebAppDemo.Models;

namespace MyTimingWebAppDemo.DataAccessLayer.Services
{
    internal class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<PersonEntity> GetAll()
        {
            return _repository.GetAll();
        }
    }
}