using System.Threading;

namespace Docker.Extensions;

static partial class Containers
{
  public static async Task<string> UseContainerAsync(
    IContainerOperations containers,
    string imageName,
    string containerName,
    Action<CreateContainerParameters>? setContainerParams = default,
    CancellationToken cancellationToken = default)
  {
    var container = await InspectContainerAsync(containers, containerName, cancellationToken);

    if(!IsExistingContainer(container))
    {
      var containerId = await CreateContainerAsync(containers, imageName, containerName, setContainerParams, cancellationToken);
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