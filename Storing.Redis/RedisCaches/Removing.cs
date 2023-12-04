using System.Threading;
using StackExchange.Redis;
using static Storing.Redis.RedisKeys;

namespace Storing.Redis;

public static partial class RedisCache {

  public static Task<bool> RemoveCacheAsync(
    IDatabase db,
    string key,
    CancellationToken cancellationToken = default) =>
      RemoveKeyAsync(db, key, cancellationToken);

}