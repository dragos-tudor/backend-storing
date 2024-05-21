using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisTests
{
  const string ClientName = "test";

  static IConnectionMultiplexer RedisClient = default!;
}