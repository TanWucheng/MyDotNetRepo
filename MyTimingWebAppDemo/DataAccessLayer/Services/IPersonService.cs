using System.Collections.Generic;
using MyTimingWebAppDemo.Models;

namespace MyTimingWebAppDemo.DataAccessLayer.Services
{
    internal interface IPersonService
    {
        IEnumerable<PersonEntity> GetAll();
    }
}