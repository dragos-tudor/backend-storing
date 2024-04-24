using Microsoft.Extensions.Caching.Distributed;
#pragma warning disable CA1032

namespace Storing.Redis;

static partial class RedisExpirations
{
  internal const string ExpirationMessage = "The absolute expiration value must be in the future.";
}

public sealed class AbsoluteExpirationException (DistributedCacheEntryOptions options) : ArgumentOutOfRangeException(
  nameof(DistributedCacheEntryOptions.AbsoluteExpiration),
  options.AbsoluteExpiration!.Value,
  RedisExpirations.ExpirationMessage)
{ }