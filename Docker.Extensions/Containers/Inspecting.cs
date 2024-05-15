using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<ContainerInspectResponse?> InspectContainerAsync(
    IContainerOperations containers,
    string containerId,
    CancellationToken cancellationToken = default)
  {
    try {
      return await containers.InspectContainerAsync(containerId, cancellationToken);
    }
    catch(DockerContainerNotFoundException) {
      return default;
    }
  }
}