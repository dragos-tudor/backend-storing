
namespace Storing.Redis;

partial class RedisTests
{
  const int ServerPort = 6379;

  [AssemblyInitialize]
  public static void InitializeRedis(TestContext _)
  {
    var networkSettings = StartRedisContainer(ServerPort, ImageName, ContainerName);
    var serverIpAddress = GetServerIpAddress(networkSettings);
    var endPoints = GetRedisEndpoints(serverIpAddress, ServerPort);

    RedisClient = CreateRedisClient(endPoints, ClientName);
  }
}