using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataModels;
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

        [HttpPost]
        public object GetTodoForDay([FromForm] MountPostPlanner mountPostPlanner)
        {
            var todoData = this.plannerService.GetPlannedTodoForToday(mountPostPlanner.Date);
            if (todoData.Any())
            {
                return todoData;
            }

            return Ok();
        }

        [HttpPost]
        public async Task<object> SaveTodoStickyNote([FromForm] TodoStickyNoteDataModel todoStickyNote)
        {
            if (await this.plannerService.SaveTodoStickyNoteAsync(todoStickyNote))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult TodoMessageBoard([FromForm] TodoStickyNote plannerData)
        {
            return PartialView("_TodoMessageBoard", plannerData);
        }
    }
}
