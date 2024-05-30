namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static DeleteResult CleanMongoCollection (IMongoDatabase db, string collName) =>
    db
    .GetCollection<BsonDocument>(collName)
    .DeleteMany(FilterDefinition<BsonDocument>.Empty);

  public static void CleanMongoCollections (IMongoDatabase db, params string[] collNames)
  {
    foreach(var collName in collNames)
      CleanMongoCollection(db, collName);
  }
}