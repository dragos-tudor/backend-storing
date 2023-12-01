using System.Threading;
using Docker.DotNet.Models;
using static Docker.Extensions.Clients;
using static Docker.Extensions.Containers;
using static Docker.Extensions.Exec;
using static Docker.Extensions.Images;

namespace Storing.SqlServer;

static partial class TestContainers
{
  const string containerName = "storing-sql";
  const string imageName = "mcr.microsoft.com/mssql/server:2017-latest";
  const int serverPort = 1433;

  static string GetServerIpAddress(NetworkSettings network) =>
    network.IPAddress;

  static async Task<string> StartSqlContainerAsync(string imageName, string containerName, int serverPort, string adminPassword)
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(10));

    await CreateDockerImageAsync(client.Images, imageName, cts.Token);
    var containerId = await StartContainerAsync(client.Containers, imageName, containerName, (@params) => @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"], cts.Token);
    var container = await InspectContainerAsync(client.Containers, containerId, cts.Token);

    await WaitForOpenPort(client.Exec, containerId, serverPort, TimeSpan.FromMinutes(3), cts.Token);
    return GetServerIpAddress(container!.NetworkSettings);
  }

  internal static Task<string> StartSqlContainerAsync(string adminPassword) =>
    StartSqlContainerAsync(imageName, containerName, serverPort, adminPassword);
}