
namespace Docker.Extensions;

partial class DockerFuncs
{
  static async Task<string> StartContainerAsync (
    IContainerOperations containers,
    string containerId,
    CancellationToken cancellationToken = default)
  {
    await containers.StartContainerAsync(containerId, new(), cancellationToken);
    return containerId;
  }

  public static async Task<string> StartContainerAsync (
    IContainerOperations containers,
    ContainerInspectResponse container,
    CancellationToken cancellationToken = default)
  {
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