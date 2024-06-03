
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static void CleanMongoDatabase (IMongoDatabase db, params string[] collNames)
  {
    CleanMongoCollections(db, collNames);
    DropAllUsers(db, CreateDropAllUsersCommand());
  }
}