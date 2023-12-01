using StackExchange.Redis;

namespace Storing.Redis;

static partial class TestDatabases
{
  public static async Task<IDatabase> GetRedisDatabase (int databaseId = 0) =>
    (await redisClient.Value).GetDatabase(databaseId);
}