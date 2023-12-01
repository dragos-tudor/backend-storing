using System.Text;
using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using static Storing.Redis.RedisHashes;

namespace Storing.Redis;

public static partial class RedisCache {

  public static Task SetCacheAsync(
    IDatabase db,
    string key,
    byte[] value,
    DistributedCacheEntryOptions? options = default,
    CancellationToken token = default) =>
      HashSetAsync(db, key, value, options ?? new DistributedCacheEntryOptions(), token);

  public static Task SetStringCacheAsync(
    IDatabase db,
    string key,
    string value,
    DistributedCacheEntryOptions? options = default,
    CancellationToken token = default) =>
      HashSetAsync(db, key, Encoding.UTF8.GetBytes(value ?? String.Empty), options ?? new DistributedCacheEntryOptions(), token);

}