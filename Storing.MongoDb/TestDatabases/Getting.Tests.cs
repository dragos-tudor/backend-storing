
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static IMongoDatabase GetMongoDatabase (MongoClient client, string dbName) => client.GetDatabase(dbName);
}