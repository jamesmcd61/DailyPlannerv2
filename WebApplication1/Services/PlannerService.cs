namespace WebApplication1.Services
{
    using Microsoft.Data.SqlClient;

    using WebApplication1.Data;
    using WebApplication1.Models;

    public interface IPlannerService
    {
        List<TodoStickyNote> GetPlannedTodoForToday(DateTime date);

        Task<bool> SaveTodoStickyNoteAsync(TodoStickyNote todoStickyNote);
    }

    public class PlannerService : IPlannerService
    {
        private readonly PlannerContext context;
        private readonly IEncryptionService encryptionService;
        private readonly string Connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DailyPlannerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public PlannerService(IEncryptionService encryptionService, PlannerContext context)
        {
            this.encryptionService = encryptionService;
            this.context = context;
        }

        public List<TodoStickyNote> GetPlannedTodoForToday(DateTime date)
        {
            var sqlConnection = new SqlConnection(this.Connection);
            var checkUser = new SqlCommand("SELECT * FROM ToDoMessages WHERE ([Date] BETWEEN @dateBefore AND @dateAfter)", sqlConnection);
            sqlConnection.Open();
            checkUser.Parameters.AddWithValue("@dateBefore", date.Date.AddDays(-1));
            checkUser.Parameters.AddWithValue("@dateAfter", date.Date.AddDays(1));
            var reader = checkUser.ExecuteReader();
            var data = new List<TodoStickyNote>();
            while (reader.Read())
            {
                data.Add(new TodoStickyNote()
                {
                    Date = DateTime.Parse(reader["Date"].ToString()),
                    Message = this.encryptionService.Deencryption(reader["Message"].ToString())
                });
            }
            sqlConnection.Close();
            return data;
        }

        public async Task<bool> SaveTodoStickyNoteAsync(TodoStickyNote todoStickyNote)
        {
            var todoNote = new TodoStickyNote()
            {
                Message = this.encryptionService.Encryption(todoStickyNote.Message),
                Date = todoStickyNote.Date
            };
            await this.context.TodoStickyNote.AddAsync(todoNote);
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
