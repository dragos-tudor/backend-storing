using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisFuncs
{
  internal static ConfigurationOptions SetConfigurationOptions (
    this ConfigurationOptions builder,
    Action<ConfigurationOptions>? configOptions = default) {
      if(configOptions != default)
        configOptions(builder);
      return builder;
    }
}