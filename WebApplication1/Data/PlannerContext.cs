namespace WebApplication1.Data
{
    using WebApplication1.DataModels;

    using Microsoft.EntityFrameworkCore;

    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) :base(options)
        {
        }

        public DbSet<LoginDataModel> Login { get; set; } = null!;

        public DbSet<RegisterDataModel> Register { get; set; } = null!;

        public DbSet<TodoStickyNoteDataModel> TodoStickyNote { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginDataModel>().ToTable("UserTable");
            modelBuilder.Entity<RegisterDataModel>().ToTable("UserInformation");
            modelBuilder.Entity<TodoStickyNoteDataModel>().ToTable("ToDoMessages");
        }
    }
}
