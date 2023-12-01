using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using static Storing.Redis.RedisExpirations;

namespace Storing.Redis;

public static partial class RedisHashes {

  public static async Task<string[]?> HashSetAsync(
    IDatabase db,
    string key,
    byte[] value,
    DistributedCacheEntryOptions options,
    CancellationToken token = default)
  {
    token.ThrowIfCancellationRequested();

    var creationTime = DateTimeOffset.UtcNow;
    if(!IsAbsoluteExpirationInFuture(creationTime, options))
      throw new AbsoluteExpirationException(options);

    var absoluteExpr = ResolveAbsoluteExpiration(creationTime, options);
    var relativeExpr = ResolveRelativeExpiration(creationTime, absoluteExpr, options);
		var slidingExpr = options.SlidingExpiration;

    return (string[]?) await db.ScriptEvaluateAsync(
      hashSetScript,
      new RedisKey[] { key },
      new RedisValue[]
      {
        absoluteExpr?.ToUnixTimeSeconds() ?? notPresent,
        (long?) slidingExpr?.TotalSeconds ?? notPresent,
        (long?) relativeExpr?.TotalSeconds ?? notPresent,
        value
      }
    );

  }

}
