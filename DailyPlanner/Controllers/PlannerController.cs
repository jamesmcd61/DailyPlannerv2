namespace DailyPlanner.Controllers
{
    using DailyPlanner.Models;
    using DailyPlanner.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PlannerController : Controller
    {
        private readonly IPlannerService plannerService;
        public PlannerController(IPlannerService plannerService) 
        { 
            this.plannerService = plannerService;
        }

        [HttpPost]
        public object GetTodoForDay([FromBody] MountPostPlanner mountPostPlanner)
        {
            var todoData = this.plannerService.GetPlannedTodoForToday(mountPostPlanner.Date);
            if (todoData.Any())
            {
                return todoData;
            }

            return Ok();
        }

        [HttpPost]
        public async Task<object> SaveTodoStickyNote([FromBody] TodoStickyNote todoStickyNote)
        {
            if (await this.plannerService.SaveTodoStickyNoteAsync(todoStickyNote))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
