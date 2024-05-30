
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task<IList<ContainerListResponse>> ListContainersAsync (IContainerOperations containers, CancellationToken cancellationToken = default) =>
    containers.ListContainersAsync(new ContainersListParameters(){ All = true }, cancellationToken);
}