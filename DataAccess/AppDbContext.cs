using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess;

public class AppDbContext : DbContext
{
    /// <summary>
    /// Constructor for AppDbContext.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
    { }

    public DbSet<teachers> teachers { get; set; }
    public DbSet<students> students { get; set; }
    public DbSet<classes> classes { get; set; }
    public DbSet<class_enrollments> class_enrollments { get; set; }
    public DbSet<grades> grades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    base.OnModelCreating(modelBuilder);

      // Explicitly map entity names to table names
     modelBuilder.Entity<teachers>().ToTable("teachers");
    modelBuilder.Entity<students>().ToTable("students");
  modelBuilder.Entity<classes>().ToTable("classes");
        modelBuilder.Entity<class_enrollments>().ToTable("class_enrollments");
        modelBuilder.Entity<grades>().ToTable("grades");
    }
}
