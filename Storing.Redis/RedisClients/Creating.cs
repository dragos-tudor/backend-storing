using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisFuncs
{
  public static IConnectionMultiplexer CreateRedisClient(RedisOptions options) => ConnectionMultiplexer.Connect(options.ConfigurationOptions);
}