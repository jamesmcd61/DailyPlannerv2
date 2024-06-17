namespace DailyPlanner.Controllers
{
    using DailyPlanner.Models;
    using DailyPlanner.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    public class AuthController : Controller
    {
        public readonly IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService) 
        {
            this.authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<object> Register([FromBody]RegisterModel register)
        {
            if (await this.authenticationService.HasRegisteredAsync(register))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<object> Login([FromBody]LoginModel login)
        {
            if (await this.authenticationService.CanLoginAsync(login))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
