
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
      Action<ConfigurationOptions>? configOptons = default)
    =>
      SetRedisConfigurationOptions(
        new ConfigurationOptions()
        {
            EndPoints = new EndPointCollection([.. ConvertToRedisEndPoints(endpoints)]),
            User = user,
            Password = password,
            DefaultDatabase = defaultDatabase,
            ConnectTimeout = (connectTimeout ?? TimeSpan.FromSeconds(15)).Seconds,
            AbortOnConnectFail = false,
            Ssl = ssl
        },
        configOptons
      );


    public static ConfigurationOptions CreateRedisConfigurationOptions(RedisOptions options) =>
      CreateRedisConfigurationOptions(
        options.EndPoints, options.User,
        options.Password, options.DefaultDatabase,
        options.Ssl, options.ConnectTimeout);
}