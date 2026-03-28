using Microsoft.EntityFrameworkCore;

namespace Arch.EFCore;

public class DataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
        base.OnConfiguring(optionsBuilder);
    }
    
    public DbSet<Note> Notes => Set<Note>();
}