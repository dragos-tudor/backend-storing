using System.Threading;
using Docker.DotNet.Models;

namespace Storing.Redis;

public static partial class TestContainers
{
  const string ImageName = "redis:7.2.3";
  const string ContainerName = "storing-redis";
  static readonly TimeSpan DockerTimeout = TimeSpan.FromMinutes(5);
  static readonly TimeSpan OpenPortTimeout = TimeSpan.FromMinutes(1);

  internal static async Task<NetworkSettings> StartRedisContainerAsync (
    int serverPort,
    string imageName = ImageName,
    string containerName = ContainerName,
    TimeSpan? dockerTimeout = default,
    TimeSpan? openPortTimeout = default)
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(dockerTimeout ?? DockerTimeout);

    await CreateDockerImageAsync(client.Images, imageName, cts.Token);
    var containerId = await UseContainerAsync(client.Containers, imageName, containerName, default, cts.Token);
    var container = await InspectContainerAsync(client.Containers, containerId, cts.Token);

    await WaitForOpenPort(client.Exec, containerId, serverPort, openPortTimeout ?? OpenPortTimeout);
    return container!.NetworkSettings;
  }
}