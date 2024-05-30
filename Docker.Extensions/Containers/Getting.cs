
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static ContainerListResponse? GetContainer (IEnumerable<ContainerListResponse> conntainerList, string containerName) =>
    conntainerList.FirstOrDefault(container => container.Names.Any(name => name == "/" + containerName));
}