global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Storing.Redis.RedisTests;

namespace Storing.Redis;

[TestClass]
public sealed partial class RedisTests
{
  [AssemblyInitialize]
  public static void InitializeRedisServer(TestContext _) => StartRedisServer();
}