using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) // Pasa las opciones al constructor base
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=app.sqlite");

    // Definir tus DbSet aquí, por ejemplo:
    public DbSet<Score> MaxScores { get; set; }
}