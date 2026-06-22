
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static string JoinReplicaSetNetworkAddresses (IEnumerable<string> networkAddresses, IEnumerable<int> ports) =>
    string.Join(",", networkAddresses.Zip(ports, GetMongoConnectionStringAuthority));
}