using System.Collections.Generic;
using MyTimingWebAppDemo.Models;

namespace MyTimingWebAppDemo.DataAccessLayer.Repositories
{
    internal interface IPersonRepository
    {
        IEnumerable<PersonEntity> GetAll();
    }
}