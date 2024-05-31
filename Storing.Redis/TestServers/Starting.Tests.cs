
using Docker.DotNet.Models;

namespace Storing.Redis;

partial class RedisTests
{
  public static async Task<ContainerInspectResponse> StartRedisServer (
    string imageName, string containerName,
    string networkName, int serverPort,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    Action<CreateContainerParameters> setCreateContainerParameters = (@params) => {
      @params.Hostname = containerName;
      @params.HostConfig = new HostConfig() { NetworkMode = networkName };
    };
    var container = await UseContainerAsync(client, imageName, containerName, setCreateContainerParameters, cancellationToken);

    await WaitForOpenPortWtihBash(client.Exec, container.ID, serverPort, cancellationToken);
    return container;
  }
}