using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MatBlazor.Model.Entity;

namespace MatBlazorDemo.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserAsync(string name, string password);
    }
}
