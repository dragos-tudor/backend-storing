using Docker.DotNet.Models;

namespace Storing.SqlServer;

partial class SqlServerTests
{
  const string ContainerName = "storing-sql";
  const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";

  static async Task<NetworkSettings> StartSqlContainerAsync (
    int serverPort,
    string adminPassword,
    string imageName,
    string containerName,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();

    await CreateDockerImageAsync(client.Images, imageName, cancellationToken);
    var containerId = await UseContainerAsync(client.Containers, imageName, containerName, SetCreateContainerParameters(adminPassword), cancellationToken);
    var container = await InspectContainerAsync(client.Containers, containerId, cancellationToken);

    await WaitForOpenPort(client.Exec, containerId, serverPort, cancellationToken);
    return container!.NetworkSettings;
  }

  static NetworkSettings StartSqlContainer (
    int serverPort,
    string adminPassword,
    string imageName,
    string containerName)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var cancellationToken = cancellationTokenSource.Token;

    return RunSynchronously(() =>
      StartSqlContainerAsync(serverPort, adminPassword, imageName, containerName, cancellationToken));
  }
}