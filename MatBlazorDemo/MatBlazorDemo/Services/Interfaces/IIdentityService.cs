using System.Threading.Tasks;

namespace MatBlazorDemo.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> LoginAsync(string name, string password);
    }
}
