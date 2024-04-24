using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using static Storing.Redis.RedisExpirations;

namespace Storing.Redis;

public static partial class RedisHashes
{
  const long NoExpiration = -1;

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
          SetHashScript,
          [ key ],
          [
            absoluteExpr?.ToUnixTimeSeconds() ?? NoExpiration,
            (long?) slidingExpr?.TotalSeconds ?? NoExpiration,
            (long?) relativeExpr?.TotalSeconds ?? NoExpiration,
            value
        ])
        .WaitAsync(cancellationToken ?? CancellationToken.None);
  }
}
