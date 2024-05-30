
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static void CleanMongoDatabase (MongoClient client, string dbName, params string[] collNames)
  {
    var database = client.GetDatabase(dbName);
    CleanMongoCollections(database, collNames);
    DropAllUsers(database, CreateDropAllUsersCommand());
  }
}