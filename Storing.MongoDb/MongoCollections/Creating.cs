
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static void CreateMongoCollection (IMongoDatabase database, string collName) => database.CreateCollection(collName);

  public static void CreateMongoCollections (IMongoDatabase database, params string[] collNames)
  {
    foreach (var collection in collNames)
      CreateMongoCollection(database, collection);
  }
}