using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  static async Task<string> UnpauseContainerAsync(
    IContainerOperations containers,
    string containerId,
    CancellationToken cancellationToken = default)
  {
    await containers.UnpauseContainerAsync(containerId, cancellationToken);
    return containerId;
  }
}