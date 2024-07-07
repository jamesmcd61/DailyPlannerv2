using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class PlannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
