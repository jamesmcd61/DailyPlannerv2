namespace WebApplication1.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TodoStickyNote
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
