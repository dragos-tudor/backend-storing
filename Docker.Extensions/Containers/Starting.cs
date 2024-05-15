using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  static async Task<string> StartContainerAsync(
    IContainerOperations containers,
    string containerId,
    CancellationToken cancellationToken = default)
  {
    await containers.StartContainerAsync(containerId, new(), cancellationToken);
    return containerId;
  }

}