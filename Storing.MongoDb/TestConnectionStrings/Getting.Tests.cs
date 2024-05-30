
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static string GetMongoConnectionString (string networkAddress, int serverPort) => $"mongodb://{networkAddress}:{serverPort}";
}