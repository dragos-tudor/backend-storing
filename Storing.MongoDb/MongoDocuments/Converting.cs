namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static BsonDocument ToBsonDocument<T> (T? document) =>
    (document is null) switch {
      true => new BsonDocument(),
      false => document.ToBsonDocument()
    };

}