using System.Threading;

namespace Docker.Extensions;

static partial class Containers
{
  static async Task<string> StartContainerAsync(
    IContainerOperations containers,
    string containerId,
    CancellationToken cancellationToken = default)
  {
    await containers.StartContainerAsync(containerId, new(), cancellationToken);
    return containerId;
  }

  public static async Task<string> StartContainerAsync(
    IContainerOperations containers,
    string imageName,
    string containerName,
    Action<CreateContainerParameters>? setContainerParams = default,
    CancellationToken cancellationToken = default)
  {
    var container = await InspectContainerAsync(containers, containerName, cancellationToken);

    if(!IsExistingContainer(container))
    {
      var createContainerParams = new CreateContainerParameters() { Image = imageName, Name = containerName };
      setContainerParams?.Invoke(createContainerParams);

      var containerId = await CreateContainerAsync(containers, createContainerParams, cancellationToken);
      return await StartContainerAsync(containers, containerId, cancellationToken);
    }
    if(IsPausedContainer(container!.State))
      return await UnpauseContainerAsync(containers, container!.ID, cancellationToken);
    if(IsKilledContainer(container!.State))
      return await StartContainerAsync(containers, container!.ID, cancellationToken);
    if(IsDeadContainer(container!.State))
      return await StartContainerAsync(containers, container!.ID, cancellationToken);
    if(IsRunningContainer(container!.State))
      return container!.ID;

    return await StartContainerAsync(containers, container!.ID, cancellationToken);
  }

}