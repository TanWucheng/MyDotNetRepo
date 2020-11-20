using EntityGeneratorWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EntityGeneratorWebApp.Controllers
{
    public class LoginController:Controller
    {
        private readonly RsaKeyPair _rsaKeyPair;

        public LoginController(IOptions<RsaKeyPair> options)
        {
            _rsaKeyPair = options.Value;
        }

        public IActionResult SignIn()
        {
            ViewData["PublicKey"] = _rsaKeyPair.PublicKey;
            return View();
        }
    }
}
