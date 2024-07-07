namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.DataModels;
    using WebApplication1.Models;
    using WebApplication1.Services;

    public class RegisterController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public RegisterController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDataModel register)
        {
            if (await this.authenticationService.HasRegisteredAsync(register))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}

