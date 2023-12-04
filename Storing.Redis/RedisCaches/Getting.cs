using System.Text;
using System.Threading;
using StackExchange.Redis;
using static Storing.Redis.RedisHashes;

namespace Storing.Redis;

public static partial class RedisCache {

  public static Task<byte[]?> GetCacheAsync(
    IDatabase db,
    string key,
    TimeProvider? timeProvider = default,
    CancellationToken cancellationToken = default) =>
      GetHashAsync(db, key, timeProvider, cancellationToken);

  public static async Task<string?> GetStringCacheAsync(
    IDatabase db,
    string key,
    TimeProvider? timeProvider = default,
    CancellationToken cancellationToken = default)
  {
    var result = await GetCacheAsync(db, key, timeProvider, cancellationToken);
    return result is not null?
      Encoding.UTF8.GetString(result):
      default;
  }

}