using Docker.DotNet.Models;
using static Docker.Extensions.Clients;
using static Docker.Extensions.Containers;
using static Docker.Extensions.Exec;
using static Docker.Extensions.Images;

namespace Storing.MongoDb;

static partial class TestContainers
{
  const string imageName = "mongo:4.2.24";
  const string containerName = "storing-mongo";
  const int serverPort = 27017;

  static string GetConnectionString(NetworkSettings network, int serverPort) =>
    $"mongodb://{network.IPAddress}:{serverPort}";

  static async Task<string> StartMongoContainerAsync(string imageName, string containerName, int serverPort)
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

    await CreateDockerImageAsync(client.Images, imageName, cts.Token);
    var containerId = await StartContainerAsync(client.Containers, imageName, containerName, default, cts.Token);
    var container = await InspectContainerAsync(client.Containers, containerId, cts.Token);

    await WaitForOpenPort(client.Exec, containerId, serverPort, TimeSpan.FromMinutes(1), cts.Token);
    return GetConnectionString(container!.NetworkSettings, serverPort);
  }

  internal static Task<string> StartMongoContainerAsync() =>
    StartMongoContainerAsync(imageName, containerName, serverPort);
}