
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static string JoinReplicaSetNetworkAddresses (IEnumerable<string> networkAddresses, int serverPort) =>
    string.Join(",", networkAddresses.Select(networkAddress => GetMongoNetworkAddressAndPort(networkAddress, serverPort)));
}