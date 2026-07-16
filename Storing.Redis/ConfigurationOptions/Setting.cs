
namespace Storing.Redis;

partial class RedisFuncs
{
    static ConfigurationOptions SetRedisConfigurationOptions(
      ConfigurationOptions options,
      Action<ConfigurationOptions>? configOptions = default)
    {
        configOptions?.Invoke(options);
        return options;
    }
}