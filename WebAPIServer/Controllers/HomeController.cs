using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Services;

namespace WebAPIServer.Controllers
{
    [ApiController]
    [Route("api/{Controller}/{Action}")]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var authResponse = userService.Authenticate(username, password);
            if (authResponse == null)
                return Unauthorized();

            return Ok(authResponse);
        }
        [Authorize]
        public IActionResult SecretArea()
        {
            return Ok("You're allowed in");
        }
    }
}