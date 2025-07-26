namespace Storing.SqlServer;

public record Author
{
  public Guid AuthorId { get; set; }
  public string AuthorName { get; set; } = string.Empty;
  public DateTime? BirthDate { get; set; }
  public ICollection<Book>? Books { get; set; }
}

public record Book
{
  public short BookId { get; set; }
  public string BookName { get; set; } = string.Empty;
  public DateTime? ReleaseDate { get; set; }
  public ICollection<Author>? Authors { get; set; }
}

public sealed class EntitiesContext(DbContextOptions<EntitiesContext> options) : DbContext(options)
{
  public DbSet<Author> Authors => Set<Author>();
  public DbSet<Book> Books => Set<Book>();
}