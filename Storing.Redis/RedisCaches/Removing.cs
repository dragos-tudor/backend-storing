using System.Threading;
using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisFuncs
{
  public static Task<bool> RemoveCacheAsync(
    IDatabase db,
    string key,
    CancellationToken cancellationToken = default) =>
      RemoveKeyAsync(db, key, cancellationToken);
}