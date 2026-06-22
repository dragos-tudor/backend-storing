
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static string JoinReplicaSetNetworkAddresses (IEnumerable<string> networkAddresses, IEnumerable<int> serverPorts) =>
    string.Join(",", networkAddresses.Zip(serverPorts, GetMongoConnectionStringAuthority));
}