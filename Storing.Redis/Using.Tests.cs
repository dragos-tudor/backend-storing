global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Storing.Redis.RedisTests;

namespace Storing.Redis;

[TestClass]
public sealed partial class RedisTests
{
  [AssemblyInitialize]
  public static void InitializeRedisServer(TestContext _)
  {
    const string imageName = "redis:7.2.3";
    const string containerName = "storing-redis";

    const string clientName = "test";
    const int serverPort = 6379;

    RedisClient = StartRedisServer(clientName, imageName, containerName, serverPort);
  }
}