
namespace Storing.MongoDb;

partial class MongoDbTests
{
  const string DbCollection = "documents";
  const string DbName = "storing";

  static string GetMongoConnectionString (string ipAddress, int serverPort) => $"mongodb://{ipAddress}:{serverPort}";

  static IMongoDatabase GetMongoDatabase (string dbName = DbName) => MongoDbClient.GetDatabase(dbName);
}