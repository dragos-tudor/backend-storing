
namespace Storing.Redis;

partial class RedisTests
{
  static IDatabase GetRedisDatabase(IConnectionMultiplexer client, int databaseId = 0) => client.GetDatabase(databaseId);
}