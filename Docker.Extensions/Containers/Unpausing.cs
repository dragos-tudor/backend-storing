using System.Threading;

namespace Docker.Extensions;

static partial class Containers
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