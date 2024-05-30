
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task<NetworkResponse> InspectNetworkAsync (INetworkOperations networks, string networkName, CancellationToken cancellationToken = default) =>
    networks.InspectNetworkAsync(networkName, cancellationToken);
}