namespace Storing.MongoDb;

public static partial class MongoCollections
{
  public static DeleteResult CleanupCollection (IMongoDatabase db, string collName) =>
    db
    .GetCollection<BsonDocument>(collName)
    .DeleteMany(FilterDefinition<BsonDocument>.Empty);

  public static void CleanupCollections (IMongoDatabase db, params string[] collNames)
  {
    foreach(var collName in collNames)
      CleanupCollection(db, collName);
  }
}