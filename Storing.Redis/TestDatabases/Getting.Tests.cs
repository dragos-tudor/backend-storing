using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisTests
{
  static string GetRedisEndpoints(string networkAddress, int serverPort) => $"{networkAddress}:{serverPort}";

  internal static IDatabase GetRedisDatabase (int databaseId = 0) => RedisClient.GetDatabase(databaseId);
}