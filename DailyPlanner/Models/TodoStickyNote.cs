using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Models
{
    public class TodoStickyNote
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
