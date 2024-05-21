using Docker.DotNet.Models;

namespace Storing.MongoDb;

partial class MongoDbTests
{
  const string ImageName = "mongo:4.2.24";
  const string ContainerName = "storing-mongo";

  static async Task<NetworkSettings> StartMongoContainerAsync (
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

  static NetworkSettings StartMongoContainer (
    int serverPort,
    string imageName,
    string containerName)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    var cancellationToken = cancellationTokenSource.Token;

    return RunSynchronously(() => StartMongoContainerAsync(serverPort, imageName, containerName, cancellationToken));
  }
}