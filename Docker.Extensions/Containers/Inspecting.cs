
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task<ContainerInspectResponse> InspectContainerAsync(IContainerOperations containers, string containerId, CancellationToken cancellationToken = default) =>
    containers.InspectContainerAsync(containerId, cancellationToken);
}