using System.Text;
using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using static Storing.Redis.RedisHashes;

namespace Storing.Redis;

public static partial class RedisCache
{
  static byte[] ToBytes(string text) => Encoding.UTF8.GetBytes(text ?? string.Empty);

  public static Task SetCacheAsync(
    IDatabase db,
    string key,
    byte[] value,
    DistributedCacheEntryOptions? options = default,
    TimeProvider? timeProvider = default,
    CancellationToken cancellationToken = default) =>
      SetHashAsync(db, key, value, options ?? new DistributedCacheEntryOptions(), timeProvider, cancellationToken);

  public static Task SetStringCacheAsync(
    IDatabase db,
    string key,
    string value,
    DistributedCacheEntryOptions? options = default,
    TimeProvider? timeProvider = default,
    CancellationToken cancellationToken = default) =>
      SetHashAsync(db, key, ToBytes(value), options ?? new DistributedCacheEntryOptions(), timeProvider, cancellationToken);
}