
using System.Net;

namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  static string GetMongoNetworkAddressAndPort (string networkAddress, int serverPort) => $"{networkAddress}:{serverPort}";

  public static string GetMongoConnectionString (string networkAddress, int serverPort) =>
    GetMongoConnectionString(networkAddress, serverPort, default!, default!);

  public static string GetMongoConnectionString (string networkAddress, int serverPort, string? userName, string? password)
  {
    var authority = GetMongoNetworkAddressAndPort(networkAddress, serverPort);

    if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
      return $"mongodb://{authority}";

    return $"mongodb://{WebUtility.UrlEncode(userName)}:{WebUtility.UrlEncode(password)}@{authority}";
  }

  public static string GetMongoConnectionString (MongoOptions options)
  {
    var userName = string.IsNullOrWhiteSpace(options.UserName) ? options.AdminName : options.UserName;
    var password = string.IsNullOrWhiteSpace(options.UserName) ? options.AdminPassword : options.UserPassword;

    return GetMongoConnectionString(options.ServerName, options.ServerPort, userName, password);
  }

  public static string GetMongoConnectionString (string networkAddresses, string replicaSet) => $"mongodb://{networkAddresses}/?replicaSet={replicaSet}";
}