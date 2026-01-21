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
    public DbSet<modules> modules { get; set; }
    
    // New tables
    public DbSet<departments> departments { get; set; }
    public DbSet<prorectors> prorectors { get; set; }
    public DbSet<teacher_prorectors> teacher_prorectors { get; set; }
    public DbSet<grade_changes> grade_changes { get; set; }
    public DbSet<teacher_classes> teacher_classes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Explicitly map entity names to table names
        modelBuilder.Entity<teachers>().ToTable("teachers");
        modelBuilder.Entity<students>().ToTable("students");
        modelBuilder.Entity<classes>().ToTable("classes");
        modelBuilder.Entity<class_enrollments>().ToTable("class_enrollments");
        modelBuilder.Entity<grades>().ToTable("grades");
        modelBuilder.Entity<modules>().ToTable("modules");
        modelBuilder.Entity<departments>().ToTable("departments");
        modelBuilder.Entity<prorectors>().ToTable("prorectors");
        modelBuilder.Entity<teacher_prorectors>().ToTable("teacher_prorectors");
        modelBuilder.Entity<grade_changes>().ToTable("grade_changes");
        modelBuilder.Entity<teacher_classes>().ToTable("teacher_classes");

        // Configure unique constraints
        modelBuilder.Entity<departments>()
      .HasIndex(d => d.department_name)
         .IsUnique();

     modelBuilder.Entity<modules>()
           .HasIndex(m => m.module_code)
    .IsUnique();

            modelBuilder.Entity<students>()
        .HasIndex(s => s.email)
           .IsUnique();

            modelBuilder.Entity<teachers>()
                .HasIndex(t => t.email)
                .IsUnique();

      modelBuilder.Entity<prorectors>()
      .HasIndex(p => p.email)
        .IsUnique();

            modelBuilder.Entity<class_enrollments>()
 .HasIndex(ce => new { ce.class_id, ce.student_id })
                .IsUnique();

        // Configure composite primary key for teacher_classes
        modelBuilder.Entity<teacher_classes>()
   .HasKey(tc => new { tc.teacher_id, tc.class_id, tc.module_id });
    }
}
