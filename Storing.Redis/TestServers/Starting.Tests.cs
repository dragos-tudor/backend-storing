
using StackExchange.Redis;

namespace Storing.Redis;

partial class RedisTests
{
  static IConnectionMultiplexer StartRedisServer (string clientName, string imageName, string containerName, int serverPort)
  {
    var networkSettings = StartRedisContainer(serverPort, imageName, containerName);
    var serverIpAddress = GetServerIpAddress(networkSettings);
    var endPoints = GetRedisEndpoints(serverIpAddress, serverPort);

    return CreateRedisClient(endPoints, clientName);
  }
}