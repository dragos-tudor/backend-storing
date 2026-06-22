
using System.Collections.ObjectModel;

namespace Storing.MongoDb;

public record MongoOptions
{
  public string UserName { get; init; } = default!;
  public string UserPassword { get; init; } = default!;
  public string DbName { get; init; } = default!;
  public int ServerPort { get; init; }
  public string ServerName { get; init; } = default!;
}

public record MongoReplicaSetOptions
{
  public string UserName { get; init; } = default!;
  public string UserPassword { get; init; } = default!;
  public string DbName { get; init; } = default!;
  public string ReplicaSet { get; init; } = default!;
  public ReadOnlyCollection<int> ServerPorts { get; init; } = [];
  public ReadOnlyCollection<string> ServerNames { get; init; } = [];
}