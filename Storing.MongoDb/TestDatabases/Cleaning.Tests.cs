
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static void CleanMongoDatabase (MongoClient client, string dbName, params string[] collections)
  {
    var database = client.GetDatabase(dbName);
    CleanupCollections(database, collections);
    DropAllUsers(database, CreateDropAllUsersCommand());
  }
}