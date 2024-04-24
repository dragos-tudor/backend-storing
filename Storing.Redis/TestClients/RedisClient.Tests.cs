using StackExchange.Redis;

namespace Storing.Redis;

static partial class TestClients
{
  const string ClientName = "test";

  internal static readonly Lazy<Task<IConnectionMultiplexer>> RedisClient =
    new (() => CreateRedisClient(ClientName));
}