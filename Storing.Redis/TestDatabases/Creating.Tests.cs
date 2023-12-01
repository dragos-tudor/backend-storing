using StackExchange.Redis;
using static Storing.Redis.RedisConnections;
using static Storing.Redis.RedisOptions;
using static Storing.Redis.TestContainers;

namespace Storing.Redis;

static partial class TestDatabases
{
  static readonly Lazy<Task<IConnectionMultiplexer>> redisClient = new (CreateRedisClient);

  static async Task<IConnectionMultiplexer> CreateRedisClient() =>
    CreateRedisConnection(
      CreateRedisOptions(
        CreateConfigurationOptions(
          await StartRedisContainerAsync(),
          clientName: "test"
        ))
    );
}