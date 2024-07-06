namespace WebApplication1.Data
{
    using WebApplication1.Models;

    using Microsoft.EntityFrameworkCore;

    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) :base(options)
        {
        }

        public DbSet<LoginModel> Login { get; set; } = null!;

        public DbSet<RegisterModel> Register { get; set; } = null!;

        public DbSet<TodoStickyNote> TodoStickyNote { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginModel>().ToTable("UserTable");
            modelBuilder.Entity<RegisterModel>().ToTable("UserInformation");
            modelBuilder.Entity<TodoStickyNote>().ToTable("ToDoMessages");
        }
    }
}
