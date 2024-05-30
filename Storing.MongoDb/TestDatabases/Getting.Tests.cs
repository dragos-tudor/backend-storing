
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static IMongoDatabase GetMongoDatabase (string dbName = DatabaseName) => MongoDbClient.GetDatabase(dbName);
}