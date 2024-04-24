using StackExchange.Redis;
using static Storing.Redis.RedisConnections;
using static Storing.Redis.RedisOptions;
using static Storing.Redis.TestContainers;

namespace Storing.Redis;

public static partial class TestClients
{
  const int ServerPort = 6379;

  internal static async Task<IConnectionMultiplexer> CreateRedisClient(string clientName) =>
    CreateRedisConnection(
      CreateRedisOptions(
        CreateConfigurationOptions(
          GetRedisEndpoints(await StartRedisContainerAsync(ServerPort), ServerPort),
          clientName: clientName
        ))
    );
}