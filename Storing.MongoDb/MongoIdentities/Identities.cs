#pragma warning disable CA1707

namespace Storing.MongoDb;

public abstract record Id { }

public abstract record Id<T>: Id
{
  [BsonId]
  public T? _Id { get; init; }
}