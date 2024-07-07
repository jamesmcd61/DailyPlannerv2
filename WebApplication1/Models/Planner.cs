namespace WebApplication1.Models
{
    public class Planner
    {
        public DateTime Day { get; set; }

        public List<TodoStickyNote> Todos { get; set; }
    }
}
