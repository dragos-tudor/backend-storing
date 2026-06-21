
namespace Storing.MongoDb;

public record MongoOptions
{
  public string AdminName { get; init; } = default!;
  public string AdminPassword { get; init; } = default!;
  public string UserName { get; init; } = default!;
  public string UserPassword { get; init; } = default!;
  public string DbName { get; init; } = default!;
  public int ServerPort { get; init; } = 27017;
  public string ServerName { get; init; } = "127.0.0.1";
}