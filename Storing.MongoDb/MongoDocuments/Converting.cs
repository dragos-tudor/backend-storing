namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static BsonDocument ToBsonDocument<T> (T? document) =>
    (document is null) switch {
      true => new BsonDocument(),
      false => document.ToBsonDocument()
    };

}