using System.Threading;
using StackExchange.Redis;

namespace Storing.Redis;

public static partial class RedisKeys {

  public static async Task<bool> RemoveKeyAsync(
    IDatabase db,
    string key,
    CancellationToken token = default) {
      token.ThrowIfCancellationRequested();

      return await db.KeyDeleteAsync(key);
    }

}