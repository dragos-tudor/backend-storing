
namespace Storing.SqlServer;

public record SqlServerOptions
{
  public string UserName { get; init; } = default!;
  public string UserPassword { get; init; } = default!;
  public string DbName { get; init; } = default!;
  public int ServerPort { get; init; }
  public string ServerName { get; init; } = default!;
}