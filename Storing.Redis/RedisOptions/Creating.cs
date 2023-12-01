using StackExchange.Redis;
using StackExchange.Redis.Profiling;

namespace Storing.Redis;

public partial class RedisOptions {

  public static ConfigurationOptions CreateConfigurationOptions(
    string endPoints,
    int? dbId = 0,
    string? clientName = default,
    Action<ConfigurationOptions>? configOptions = default) =>
      new ConfigurationOptions {
        EndPoints = { endPoints },
        DefaultDatabase = dbId,
        ClientName = clientName
      }
      .TrySetConfigurationOptions(configOptions);

  public static ConfigurationOptions CreateConfigurationOptions(
    string userName,
    string userPassword,
    string endPoints,
    int? dbId = 0,
    string? clientName = default,
    Action<ConfigurationOptions>? configOptions = default) =>
      new ConfigurationOptions {
        EndPoints = { endPoints },
        User = userName,
        Password = userPassword,
        DefaultDatabase = dbId,
        ClientName = clientName
      }
      .TrySetConfigurationOptions(configOptions);

  public static RedisOptions CreateRedisOptions(
    ConfigurationOptions options,
    Func<ProfilingSession>? profilingSession = default) =>
      new (){
        ConfigurationOptions = options,
        InstanceName = options.ClientName,
        ProfilingSession = profilingSession
      };

}