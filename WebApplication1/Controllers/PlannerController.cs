using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class PlannerController : Controller
    {
        private readonly IPlannerService plannerService;

        public PlannerController(IPlannerService plannerService)
        {
            this.plannerService = plannerService;
        }

        public IActionResult Index()
        {
            var todos = this.plannerService.GetPlannedTodoForToday(DateTime.Now);
            var vm = new Planner
            {
                Day = DateTime.Now,
                Todos = todos
            };
            return View(vm);
        }
    }
}
