using System.Threading;

namespace Docker.Extensions;

partial class Containers
{
  internal static async Task<ContainerState?> GetContainerStateAsync(IContainerOperations containers, string containerId, CancellationToken cancellationToken = default) =>
    (await InspectContainerAsync(containers, containerId, cancellationToken))?.State;
}