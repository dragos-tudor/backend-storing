using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisFuncs
{
  public static IConnectionMultiplexer CreateRedisConnection(RedisOptions options)
  {
    var connection = ConnectionMultiplexer.Connect(options.ConfigurationOptions);
    if(options.ProfilingSession is null)
      return connection;

    connection.RegisterProfiler(options.ProfilingSession);
    return connection;
  }
}