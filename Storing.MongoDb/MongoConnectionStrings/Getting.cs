
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static string GetMongoConnectionString (string networkAddress, int serverPort) => $"mongodb://{networkAddress}:{serverPort}";
}