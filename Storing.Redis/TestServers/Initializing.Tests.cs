
using Docker.DotNet.Models;

namespace Storing.Redis;

public sealed partial class RedisTests
{
  public static async Task<ContainerInspectResponse> InitializeRedisServer (
    string imageName, string containerName,
    string clientName, string networkName, int serverPort,
    CancellationToken cancellationToken = default)
  {
    var container = await StartRedisServer(imageName, containerName, networkName, serverPort, cancellationToken);
    var endPoints = GetRedisEndpoints(containerName, serverPort);
    RedisClient = CreateRedisClient(endPoints, clientName);

    return container;
  }
}