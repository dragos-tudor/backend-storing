
namespace Storing.MongoDb;

public record MongoReplicaSetOptions
{
  public IEnumerable<string> Hosts { get; init; } = [];
  public IEnumerable<int> Ports { get; init; } = [];
  public string DbName { get; init; } = default!;
  public string ReplicaSet { get; init; } = default!;
  public string UserName { get; init; } = default!;
  public string UserPassword { get; init; } = default!;
}