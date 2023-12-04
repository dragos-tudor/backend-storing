using System.Threading;
using StackExchange.Redis;

namespace Storing.Redis;

public static partial class RedisHashes {

  public static async Task<byte[]?> GetHashAsync(
    IDatabase db,
    string key,
    TimeProvider? timeProvider = default,
    CancellationToken? cancellationToken = default)
  {
    cancellationToken?.ThrowIfCancellationRequested();
		var absoluteExpr = (timeProvider ?? TimeProvider.System).GetUtcNow();

    return (byte[]?) await db.ScriptEvaluateAsync(
      getHashScript,
      [ key ],
      [ absoluteExpr.ToUnixTimeSeconds() ]
    );
  }

}
