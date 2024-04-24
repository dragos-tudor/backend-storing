using Microsoft.Extensions.Caching.Distributed;

namespace Storing.Redis;

static partial class RedisExpirations
{
  internal static bool IsAbsoluteExpirationInFuture (
    DateTimeOffset creationTime,
    DistributedCacheEntryOptions options) =>
      options.AbsoluteExpiration is null ||
      options.AbsoluteExpiration > creationTime;
}