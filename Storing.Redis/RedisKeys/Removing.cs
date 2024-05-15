using System.Threading;
using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisFuncs
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