using Microsoft.Extensions.Caching.Distributed;

namespace Storing.Redis;

static partial class RedisExpirations {

  public sealed class AbsoluteExpirationException : ArgumentOutOfRangeException {

    public AbsoluteExpirationException(DistributedCacheEntryOptions options):
      base(
        nameof(DistributedCacheEntryOptions.AbsoluteExpiration),
        options.AbsoluteExpiration!.Value,
        "The absolute expiration value must be in the future.")
    { }

  }

}