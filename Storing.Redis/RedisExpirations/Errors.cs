using Microsoft.Extensions.Caching.Distributed;

namespace Storing.Redis;

static partial class RedisExpirations
{
  const string expirationMessage = "The absolute expiration value must be in the future.";

  public sealed class AbsoluteExpirationException(DistributedCacheEntryOptions options) : ArgumentOutOfRangeException(
    nameof(DistributedCacheEntryOptions.AbsoluteExpiration),
    options.AbsoluteExpiration!.Value,
    expirationMessage)
  {}

}