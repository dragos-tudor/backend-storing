using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisTests
{
  static IConnectionMultiplexer RedisClient = default!;
}