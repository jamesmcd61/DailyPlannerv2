namespace WebApplication1.DataModels
{
    public class PlannerDataModel
    {
        public int Id { get; set; }

        public DateTime Day { get; set; }

        public List<TodoStickyNoteDataModel> Todos { get; set; }
    }
}
