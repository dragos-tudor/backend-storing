
using Docker.DotNet.Models;

namespace Storing.SqlServer;

partial class SqlServerTests
{
  static async Task<ContainerInspectResponse> StartSqlServer (
    string adminPassword, string imageName,
    string containerName, string networkName, int serverPort,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    Action<CreateContainerParameters> setCreateContainerParameters = (@params) => {
      @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"];
      @params.Hostname = containerName;
      @params.HostConfig = new HostConfig() { NetworkMode = networkName };
    };
    var container = await UseContainerAsync(client, imageName, containerName, setCreateContainerParameters, cancellationToken);

    await WaitForOpenPortWtihBash(client.Exec, container.ID, serverPort, cancellationToken);
    return container;
  }
}