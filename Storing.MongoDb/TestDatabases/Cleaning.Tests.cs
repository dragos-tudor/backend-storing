using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoUsers;

namespace Storing.MongoDb;

static partial class TestDatabases
{
  static void CleanMongoDatabase (MongoClient client)
  {
    var database = client.GetDatabase("storing");
    CleanupCollections(database, dbCollection);
    DropAllUsers(database, GetDropAllUsersCommand());
  }
}