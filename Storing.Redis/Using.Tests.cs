global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Storing.Redis.RedisTests;
global using System.Threading;

namespace Storing.Redis;

[TestClass]
public sealed partial class RedisTests
{
  [AssemblyInitialize]
  public static void InitializeRedisServer(TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      InitializeRedisServer(
        "redis:7.2.3", "storing-redis", "test",
        "storing-network", 6379,
        cancellationToken));
  }
}