
namespace Storing.Redis;

partial class RedisTests
{
  const string ImageName = "redis:7.2.3";
  const string ContainerName = "storing-redis";

  const string ClientName = "test";
  const int ServerPort = 6379;

  static void StartRedisServer ()
  {
    var networkSettings = StartRedisContainer(ServerPort, ImageName, ContainerName);
    var serverIpAddress = GetServerIpAddress(networkSettings);
    var endPoints = GetRedisEndpoints(serverIpAddress, ServerPort);

    RedisClient = CreateRedisClient(endPoints, ClientName);
  }
}