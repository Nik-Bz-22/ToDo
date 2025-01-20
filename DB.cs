namespace ToDo;

using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public DbSet<Task> Task { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={Path.Combine(Config.ROOT_DIR, "app.db")}");
    }
}


