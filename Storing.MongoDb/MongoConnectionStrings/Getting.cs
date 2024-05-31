
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  static string GetMongoNetworkAddressAndPort (string networkAddress, int serverPort) => $"{networkAddress}:{serverPort}";

  public static string GetMongoConnectionString (string networkAddress, int serverPort) => $"mongodb://{networkAddress}:{serverPort}";

  public static string GetMongoConnectionString (string networkAddresses, string replicaSet) => $"mongodb://{networkAddresses}/?replicaSet={replicaSet}";
}