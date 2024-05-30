
using System.Threading;

namespace Storing.Redis;

partial class RedisTests
{
  public static async Task<string> StartRedisServer (string imageName, string containerName, int serverPort, CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    var container = await UseContainerAsync(client, imageName, containerName, default, cancellationToken);

    await WaitForOpenPortWtihBash(client.Exec, container.ID, serverPort, cancellationToken);
    return GetNetworkIpAddress(container.NetworkSettings);
  }
}