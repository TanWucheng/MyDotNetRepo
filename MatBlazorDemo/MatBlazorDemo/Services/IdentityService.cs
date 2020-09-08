using System.Threading.Tasks;
using MatBlazorDemo.Services.Interfaces;

namespace MatBlazorDemo.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserService _userSerice;

        public IdentityService(IUserService userService)
        {
            _userSerice = userService;
        }

        public Task<bool> LoginAsync(string name, string password)
        {
            return Task.Run(async () =>
            {
                var user = await _userSerice.GetUserAsync(name, password);
                return user != null;
            });
        }
    }
}
