using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=app.sqlite");

    public DbSet<Score> MaxScores { get; set; }
}