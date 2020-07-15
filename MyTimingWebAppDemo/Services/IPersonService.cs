using System.Collections.Generic;
using MyTimingWebAppDemo.Models;

namespace MyTimingWebAppDemo.Services
{
    internal interface IPersonService
    {
        IEnumerable<PersonEntity> GetAll();
    }
}