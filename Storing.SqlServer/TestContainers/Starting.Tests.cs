using System.Threading;
using Docker.DotNet.Models;

namespace Storing.SqlServer;

static partial class TestContainers
{
  const string ContainerName = "storing-sql";
  const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";

  static readonly TimeSpan DockerTimeout = TimeSpan.FromMinutes(10);
  static readonly TimeSpan OpenPortTimeout = TimeSpan.FromMinutes(3);

  internal static async Task<NetworkSettings> StartSqlContainerAsync(
    int serverPort,
    string adminPassword,
    string imageName = ImageName,
    string containerName = ContainerName,
    TimeSpan? dockerTimeout = default,
    TimeSpan? openPortTimeout = default)
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(dockerTimeout ?? DockerTimeout);

    await CreateDockerImageAsync(client.Images, imageName, cts.Token);
    var containerId = await UseContainerAsync(client.Containers, imageName, containerName, (@params) => {
      @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"];
    }, cts.Token);
    var container = await InspectContainerAsync(client.Containers, containerId, cts.Token);

    await WaitForOpenPort(client.Exec, containerId, serverPort, openPortTimeout ?? OpenPortTimeout);
    return container!.NetworkSettings;
  }
}