namespace WebApplication1.DataModels
{
    using System.ComponentModel.DataAnnotations;

    public class TodoStickyNoteDataModel
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
