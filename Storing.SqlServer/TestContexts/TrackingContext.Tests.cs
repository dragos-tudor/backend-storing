using System.ComponentModel.DataAnnotations.Schema;

namespace Storing.SqlServer;

public record Student
{
  public int StudentId { get; set; }
  public ICollection<Course>? Courses { get; set; }
}

public record Course
{
  public int CourseId { get; set; }
  public ICollection<Student>? Students { get; set; }
  public Professor? Professor { get; set; }
}

public record Professor
{
  public int ProfessorId { get; set; }
  [NotMapped] public object? NonEntity { get; set; }
  [NotMapped] public ICollection<object>? NonEntities { get; set; }
}

public sealed class TrackingContext(DbContextOptions<TrackingContext> options) : DbContext(options)
{
  public DbSet<Student> Students => Set<Student>();
  public DbSet<Course> Courses => Set<Course>();
  public DbSet<Professor> Professors => Set<Professor>();
}