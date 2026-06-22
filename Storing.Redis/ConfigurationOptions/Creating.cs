
namespace Storing.Redis;

partial class RedisFuncs
{
  public static ConfigurationOptions CreateRedisConfigurationOptions(
    IEnumerable<string> endpoints,
    string? user = default,
    string? password = default,
    int? defaultDatabase = default,
    bool ssl = false,
    TimeSpan? connectTimeout = default,
    Action<ConfigurationOptions>? configBuilder = default)
  =>
    SetRedisConfigurationOptions(
      new ConfigurationOptions()
      {
        EndPoints = new EndPointCollection([.. ConvertToRedisEndPoints(endpoints)]),
        User = user,
        Password = password,
        DefaultDatabase = defaultDatabase,
        ConnectTimeout = (connectTimeout?? TimeSpan.FromSeconds(15)).Seconds,
        AbortOnConnectFail = false,
        Ssl = ssl
      },
      configBuilder
    );


  public static ConfigurationOptions CreateConfigurationOptions(RedisOptions options) =>
    CreateRedisConfigurationOptions(
      options.EndPoints, options.User,
      options.Password, options.DefaultDatabase,
      options.Ssl, options.ConnectTimeout);
}