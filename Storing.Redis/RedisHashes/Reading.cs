using System.Threading;
using StackExchange.Redis;

namespace Storing.Redis;

public static partial class RedisHashes {

  public static async Task<byte[]?> HashGetAsync(
    IDatabase db,
    string key,
    CancellationToken token = default)
  {
    token.ThrowIfCancellationRequested();
		var absoluteExpr = DateTimeOffset.UtcNow;
		
    return (byte[]?) await db.ScriptEvaluateAsync(
      hashGetScript,
      new RedisKey[] { key },
      new RedisValue[] { absoluteExpr.ToUnixTimeSeconds() }
    );
  }

}
