
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static string GetMongoConnectionString (string ipAddress, int serverPort) => $"mongodb://{ipAddress}:{serverPort}";

  static IMongoDatabase GetMongoDatabase (string dbName = MongoDatabaseName) => MongoDbClient.GetDatabase(dbName);
}