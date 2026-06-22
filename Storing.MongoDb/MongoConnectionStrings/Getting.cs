
using System.Net;

namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  static string GetMongoConnectionStringAuthority (string networkAddress, int serverPort) => $"{networkAddress}:{serverPort}";

  static string GetMongoConnectionStringAt (string? credentials) => !string.IsNullOrEmpty(credentials) ? $"@" : string.Empty;

  static string GetMongoConnectionStringReplicaSet (string? replicaSet) => !string.IsNullOrEmpty(replicaSet) ? $"?replicaSet={WebUtility.UrlEncode(replicaSet)}" : string.Empty;

  static string GetMongoConnectionString (string authority, string? credentials = default, string? replicaSet = default) =>
    $"mongodb://{credentials}{GetMongoConnectionStringAt(credentials)}{authority}{GetMongoConnectionStringReplicaSet(replicaSet)}";


  public static string GetMongoConnectionString (string networkAddress, int serverPort, string? userName = default, string? password = default)
  {
    var authority = GetMongoConnectionStringAuthority(networkAddress, serverPort);
    string credentials = GetMongoCredentials(userName, password);
    return GetMongoConnectionString(authority, credentials);
  }

  public static string GetMongoConnectionString (IEnumerable<string> networkAddresses, IEnumerable<int> serverPorts, string replicaSet, string? userName = default, string? password = default)
  {
    var authorities = JoinReplicaSetNetworkAddresses(networkAddresses, serverPorts);
    string credentials = GetMongoCredentials(userName, password);
    return GetMongoConnectionString(authorities, credentials, replicaSet);
  }


  public static string GetMongoConnectionString (MongoOptions options) =>
    GetMongoConnectionString(options.ServerName, options.ServerPort, options.UserName, options.UserPassword);

  public static string GetMongoConnectionString (MongoReplicaSetOptions options) =>
    GetMongoConnectionString(options.ServerNames, options.ServerPorts, options.ReplicaSet, options.UserName, options.UserPassword);
}