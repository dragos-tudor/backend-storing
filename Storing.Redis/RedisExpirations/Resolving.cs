using Microsoft.Extensions.Caching.Distributed;

namespace Storing.Redis;

static partial class RedisExpirations {

  static TimeSpan GetMinimumExpiration(
    DateTimeOffset creationTime,
    DateTimeOffset absoluteExpr,
    TimeSpan slidingExpr) {
      var relativeExpr = absoluteExpr - creationTime;
      return relativeExpr < slidingExpr?
        relativeExpr:
        slidingExpr;
    }

  internal static DateTimeOffset? ResolveAbsoluteExpiration(
    DateTimeOffset creationTime,
    DistributedCacheEntryOptions options) =>
      options.AbsoluteExpirationRelativeToNow is not null?
        creationTime + options.AbsoluteExpirationRelativeToNow:
        options.AbsoluteExpiration;

  internal static TimeSpan? ResolveRelativeExpiration(
    DateTimeOffset creationTime,
    DateTimeOffset? absoluteExpr,
    DistributedCacheEntryOptions options)
  {
    var slidingExpr = options.SlidingExpiration;
    return (absoluteExpr, slidingExpr) switch {
      (not null, not null) => GetMinimumExpiration(creationTime, absoluteExpr.Value, slidingExpr.Value),
      (not null, null) => absoluteExpr.Value - creationTime,
      (null, not null) => slidingExpr.Value,
      _ => null
    };
  }

}