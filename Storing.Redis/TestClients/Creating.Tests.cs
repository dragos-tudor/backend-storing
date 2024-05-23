using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisTests
{
  static IConnectionMultiplexer CreateRedisClient (string endPoints, string clientName) =>
    CreateRedisConnection(
      CreateRedisOptions(
        CreateConfigurationOptions(
          endPoints,
          clientName: clientName
        ))
    );
}