
using Docker.DotNet.Models;

namespace Storing.SqlServer;

partial class SqlServerTests
{
  static async Task<string> StartSqlServer (string adminPassword, string imageName, string containerName, int serverPort, CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    Action<CreateContainerParameters> setCreateContainerParameters = (@params) =>
      @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"];
    var container = await UseContainerAsync(client, imageName, containerName, setCreateContainerParameters, cancellationToken);

    await WaitForOpenPortWtihBash(client.Exec, container.ID, serverPort, cancellationToken);
    return GetNetworkIpAddress(container.NetworkSettings);
  }
}