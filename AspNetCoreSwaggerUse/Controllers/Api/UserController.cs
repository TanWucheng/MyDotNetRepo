using System.Threading.Tasks;
using AspNetCoreSwaggerUse.Models;
using AspNetCoreSwaggerUse.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSwaggerUse.Controllers.Api
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserAsync(id);
            return new JsonResult(user);
        }

        [HttpPut("put")]
        public async Task<int> Put([FromBody] User user)
        {
            var count = await _userService.InsertOneAsync(user);
            return count;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var users = await _userService.GetAllUserAsync();
            return new JsonResult(users);
        }

        [HttpDelete("delete")]
        public async Task<int> Delete(int id)
        {
            return await _userService.DeleteUserAsync(id);
        }
    }
}