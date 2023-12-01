using System.Text;
using System.Threading;
using StackExchange.Redis;
using static Storing.Redis.RedisHashes;

namespace Storing.Redis;

public static partial class RedisCache {

  public static Task<byte[]?> GetCacheAsync(
    IDatabase db,
    string key,
    CancellationToken token = default) =>
      HashGetAsync(db, key, token);

  public static async Task<string?> GetStringCacheAsync(
    IDatabase db,
    string key,
    CancellationToken token = default) {
      var result = await HashGetAsync(db, key, token);
      return result is not null?
        Encoding.UTF8.GetString(result):
        default;
    }

}