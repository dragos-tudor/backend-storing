
namespace Storing.Redis;

partial class RedisFuncs
{
    public static IConnectionMultiplexer CreateRedisClient(ConfigurationOptions options) => ConnectionMultiplexer.Connect(options);

    public static IConnectionMultiplexer CreateRedisClient(IEnumerable<string> endPoints, string? user = default, string? password = default, int? defaultDatabase = default) =>
      CreateRedisClient(CreateRedisConfigurationOptions(endPoints, user, password, defaultDatabase));
}