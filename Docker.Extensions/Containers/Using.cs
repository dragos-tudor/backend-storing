
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<ContainerInspectResponse> UseContainerAsync (
    IDockerClient client,
    string imageName,
    string containerName,
    Action<CreateContainerParameters>? setCreateContainerParameters = default,
    CancellationToken cancellationToken = default)
  {
    await CreateDockerImageAsync(client.Images, imageName, cancellationToken);

    var containerList = await ListContainersAsync(client.Containers, cancellationToken);
    var containerListItem = GetContainer(containerList, containerName);
    if(!ExistContainer(containerListItem))
      await CreateContainerAsync(client.Containers, imageName, containerName, setCreateContainerParameters, cancellationToken);

    var containerInspect = await InspectContainerAsync(client.Containers, containerName, cancellationToken);
    var containerId = await StartContainerAsync(client.Containers, containerInspect!, cancellationToken);
    return (await InspectContainerAsync(client.Containers, containerId, cancellationToken))!;
  }
}