using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Model
{
    public class TasksContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Tasks.db");
        }
    }
}