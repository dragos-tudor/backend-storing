using System.Threading;
using Docker.DotNet.Models;
using static Docker.Extensions.Clients;
using static Docker.Extensions.Containers;
using static Docker.Extensions.Exec;
using static Docker.Extensions.Images;

namespace Storing.Redis;

static partial class TestContainers
{
  const string imageName = "redis:7.2.3";
  const string containerName = "storing-redis";
  const int serverPort = 6379;

  static string GetMongoEndpoints(NetworkSettings network, int serverPort) =>
    $"{network.IPAddress}:{serverPort}";

  static async Task<string> StartRedisContainerAsync(string imageName, string containerName, int serverPort)
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

    await CreateDockerImageAsync(client.Images, imageName, cts.Token);
    var containerId = await StartContainerAsync(client.Containers, imageName, containerName, default, cts.Token);
    var container = await InspectContainerAsync(client.Containers, containerId, cts.Token);

    await WaitForOpenPort(client.Exec, containerId, serverPort, TimeSpan.FromMinutes(1), cts.Token);
    return GetMongoEndpoints(container!.NetworkSettings, serverPort);
  }

  internal static Task<string> StartRedisContainerAsync() =>
    StartRedisContainerAsync(imageName, containerName, serverPort);
}