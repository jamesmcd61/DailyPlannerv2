namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using WebApplication1.DataModels;
    using WebApplication1.Services;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAuthenticationService authenticationService;

        public HomeController(
            ILogger<HomeController> logger,
            IAuthenticationService authenticationService)
        {
            this.logger = logger;
            this.authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<object> Login([FromForm] LoginDataModel login)
        {
            if (await this.authenticationService.CanLoginAsync(login))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}