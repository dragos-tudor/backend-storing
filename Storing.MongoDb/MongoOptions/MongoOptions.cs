
namespace Storing.MongoDb;

public record MongoOptions
{
  public string Host { get; init; } = default!;
  public int Port { get; init; }
  public string DbName { get; init; } = default!;
  public string UserName { get; init; } = default!;
  public string UserPassword { get; init; } = default!;
}