using Microsoft.Extensions.Caching.Distributed;

namespace Storing.Redis;

partial class RedisFuncs
{
  internal static bool IsAbsoluteExpirationInFuture (
    DateTimeOffset creationTime,
    DistributedCacheEntryOptions options) =>
      options.AbsoluteExpiration is null ||
      options.AbsoluteExpiration > creationTime;
}