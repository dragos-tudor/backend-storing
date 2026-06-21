
namespace Storing.SqlServer;

public record SqlServerOptions
{
  public string AdminName { get; init; } = "sa";
  public string AdminPassword { get; init; } = default!;
  public string UserName { get; init; } = default!;
  public string UserPassword { get; init; } = default!;
  public string DbName { get; init; } = default!;
  public string ServerName { get; init; } = "127.0.0.1";
}