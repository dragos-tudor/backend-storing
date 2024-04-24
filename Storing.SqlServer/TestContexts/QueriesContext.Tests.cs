namespace Storing.SqlServer;
#pragma warning disable CA1720

public record Filter
{
  public int FilterId { get; init; }
  public int Int { get; init; }
  public string Text { get; init; } = string.Empty;
  public bool? Bool { get; init; }
  public DateTime? Date { get; init; }
}

public record Order
{
  public int OrderId { get; init; }
  public string Text { get; init; } = string.Empty;
}

public record Page
{
  public int PageId { get; init; }
  public string Text { get; init; } = string.Empty;
}


public sealed class QueriesContext: DbContext
{
  public DbSet<Filter> Filters => Set<Filter>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<Page> Pages => Set<Page>();

  public QueriesContext(DbContextOptions<QueriesContext> options): base(options) {
    ChangeTracker.LazyLoadingEnabled = false;
    Database.EnsureCreated();
  }
}