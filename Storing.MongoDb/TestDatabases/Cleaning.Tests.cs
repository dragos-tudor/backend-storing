using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoUsers;

namespace Storing.MongoDb;

static partial class TestDatabases
{
  internal static void CleanMongoDatabase (
    MongoClient client,
    string dbName,
    params string[] collections)
  {
    var database = client.GetDatabase(dbName);
    CleanupCollections(database, collections);
    DropAllUsers(database, CreateDropAllUsersCommand());
  }
}