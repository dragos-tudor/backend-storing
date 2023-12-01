namespace Storing.MongoDb;

public static partial class MongoIdentities {

  public abstract record Id { }

  public abstract record Id<T> : Id {
    public T? _id { get; init; }

  }

}