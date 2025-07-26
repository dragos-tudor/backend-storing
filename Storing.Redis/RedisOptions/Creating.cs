using StackExchange.Redis;
using StackExchange.Redis.Profiling;

namespace Storing.Redis;

partial class RedisFuncs
{
  public static ConfigurationOptions CreateConfigurationOptions(
    string endPoints,
    int? dbId = 0,
    string? clientName = default,
    Action<ConfigurationOptions>? configOptions = default) =>
      new ConfigurationOptions
      {
        EndPoints = { endPoints },
        DefaultDatabase = dbId,
        ClientName = clientName
      }
      .SetConfigurationOptions(configOptions);

  public static ConfigurationOptions CreateConfigurationOptions(
    string userName,
    string userPassword,
    string endPoints,
    int? dbId = 0,
    string? clientName = default,
    Action<ConfigurationOptions>? configOptions = default) =>
      new ConfigurationOptions
      {
        EndPoints = { endPoints },
        User = userName,
        Password = userPassword,
        DefaultDatabase = dbId,
        ClientName = clientName
      }
      .SetConfigurationOptions(configOptions);

  public static RedisOptions CreateRedisOptions(
    ConfigurationOptions options,
    Func<ProfilingSession>? profilingSession = default) =>
      new()
      {
        ConfigurationOptions = options,
        InstanceName = options.ClientName,
        ProfilingSession = profilingSession
      };
}