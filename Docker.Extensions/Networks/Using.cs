
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<string> UseNetworkAsync (INetworkOperations networks, string networkName, CancellationToken cancellationToken = default)
  {
    var networkList = await ListNetworksAsync(networks, cancellationToken);
    var networkListItem = GetNetwork(networkList, networkName);
    if (ExistNetwork(networkListItem)) return networkListItem!.ID;

    var networkCreated = await CreateNetworkAsync(networks, networkName, cancellationToken);
    return networkCreated.ID;
  }
}