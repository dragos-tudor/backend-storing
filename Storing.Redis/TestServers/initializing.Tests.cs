
using System.Threading;

namespace Storing.Redis;

public sealed partial class RedisTests
{
  public static async Task<string> InitializeRedisServer (string imageName, string containerName, string clientName, int serverPort, CancellationToken cancellationToken = default)
  {
    var networkAddress = await StartRedisServer(imageName, containerName, serverPort, cancellationToken);
    var endPoints = GetRedisEndpoints(networkAddress, serverPort);
    RedisClient = CreateRedisClient(endPoints, clientName);

    return networkAddress;
  }
}