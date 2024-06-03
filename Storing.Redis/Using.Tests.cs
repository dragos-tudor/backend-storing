global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Storing.Redis.RedisTests;
global using System.Threading;
using StackExchange.Redis;

namespace Storing.Redis;

[TestClass]
public sealed partial class RedisTests
{
  internal static readonly IDatabase Database = GetRedisDatabase(CreateRedisClient(GetRedisEndpoints("storing-redis", 6379), "test"));

  [AssemblyInitialize]
  public static void InitializeRedisServer(TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      StartRedisServer(
        "redis:7.2.3", "storing-redis",
        "storing-network", 6379,
        cancellationToken));
  }
}