using System.Threading;
using StackExchange.Redis;

namespace Storing.Redis;

public static partial class RedisKeys
{
  public static Task<bool> RemoveKeyAsync(
    IDatabase db,
    string key,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();
    return db.KeyDeleteAsync(key);
  }
}