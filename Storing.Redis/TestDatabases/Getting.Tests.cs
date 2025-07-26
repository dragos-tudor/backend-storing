
using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisTests
{
  static string GetRedisEndpoints(string networkAddress, int serverPort) => $"{networkAddress}:{serverPort}";

  static IDatabase GetRedisDatabase(IConnectionMultiplexer client, int dbId = 0) => client.GetDatabase(dbId);
}