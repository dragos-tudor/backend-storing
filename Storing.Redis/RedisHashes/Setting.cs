using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using static Storing.Redis.RedisExpirations;

namespace Storing.Redis;

public static partial class RedisHashes
{
  const long noExpiration = -1;

  public static async Task<string[]?> SetHashAsync(
    IDatabase db,
    string key,
    byte[] value,
    DistributedCacheEntryOptions options,
    TimeProvider? timeProvider = default,
    CancellationToken? cancellationToken = default)
  {
    var creationTime = (timeProvider ?? TimeProvider.System).GetUtcNow();
    if(!IsAbsoluteExpirationInFuture(creationTime, options))
      throw new AbsoluteExpirationException(options);

    var absoluteExpr = ResolveAbsoluteExpiration(creationTime, options);
    var relativeExpr = ResolveRelativeExpiration(creationTime, absoluteExpr, options);
		var slidingExpr = options.SlidingExpiration;

    return (string[]?)
      await db
        .ScriptEvaluateAsync(
          setHashScript,
          [ key ],
          [
            absoluteExpr?.ToUnixTimeSeconds() ?? noExpiration,
            (long?) slidingExpr?.TotalSeconds ?? noExpiration,
            (long?) relativeExpr?.TotalSeconds ?? noExpiration,
            value
        ])
        .WaitAsync(cancellationToken ?? CancellationToken.None);
  }

}
