namespace WebApplication1.Models
{
    public class Planner
    {
        public int Id { get; set; }

        public DateTime Day { get; set; }

        public List<TodoStickyNote> Todos { get; set; }
    }
}
