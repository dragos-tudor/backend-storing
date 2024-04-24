using StackExchange.Redis;

namespace Storing.Redis;

public static class RedisOptionsExtensions
{
  internal static ConfigurationOptions TrySetConfigurationOptions (
    this ConfigurationOptions builder,
    Action<ConfigurationOptions>? configOptions = default) {
      if(configOptions != default)
        configOptions(builder);
      return builder;
    }
}