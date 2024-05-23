using System.Threading;
using Docker.DotNet.Models;

namespace Storing.Redis;

partial class RedisTests
{
  static async Task<NetworkSettings> StartRedisContainer (
    int serverPort,
    string imageName,
    string containerName,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();

    await CreateDockerImageAsync(client.Images, imageName, cancellationToken);
    var containerId = await UseContainerAsync(client.Containers, imageName, containerName, default, cancellationToken);
    var container = await InspectContainerAsync(client.Containers, containerId, cancellationToken);

    await WaitForOpenPort(client.Exec, containerId, serverPort, cancellationToken);
    return container!.NetworkSettings;
  }

  static NetworkSettings StartRedisContainer (
    int serverPort,
    string imageName,
    string containerName)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    var cancellationToken = cancellationTokenSource.Token;

    return RunSynchronously(() => StartRedisContainer(serverPort, imageName, containerName, cancellationToken));
  }
}