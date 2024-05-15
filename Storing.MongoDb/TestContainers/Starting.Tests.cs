using Docker.DotNet.Models;

namespace Storing.MongoDb;

static partial class TestContainers
{
  const string ImageName = "mongo:4.2.24";
  const string ContainerName = "storing-mongo";
  static readonly TimeSpan DockerTimeout = TimeSpan.FromMinutes(5);
  static readonly TimeSpan OpenPortTimeout = TimeSpan.FromMinutes(1);

  internal static async Task<NetworkSettings> StartMongoContainerAsync (
    int serverPort,
    string imageName = ImageName,
    string containerName = ContainerName,
    TimeSpan? dockerTimeout = default,
    TimeSpan? openPortTimeout = default)
  {
    using var client = CreateDockerClient();
    using var dockerToken = new CancellationTokenSource(dockerTimeout ?? DockerTimeout);

    await CreateDockerImageAsync(client.Images, imageName, dockerToken.Token);
    var containerId = await UseContainerAsync(client.Containers, imageName, containerName, default, dockerToken.Token);
    var container = await InspectContainerAsync(client.Containers, containerId, dockerToken.Token);

    await WaitForOpenPort(client.Exec, containerId, serverPort, openPortTimeout ?? OpenPortTimeout);
    return container!.NetworkSettings;
  }
}