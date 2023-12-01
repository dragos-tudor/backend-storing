namespace Storing.MongoDb;

public static partial class MongoDocuments {

  public static BsonDocument ToBsonDocument<T> (T? document) =>
    (document is null) switch {
      true => new BsonDocument(),
      false => document.ToBsonDocument()
    };

}