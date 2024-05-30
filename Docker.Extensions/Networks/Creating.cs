
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task<NetworksCreateResponse> CreateNetworkAsync (INetworkOperations networks, string networkName, CancellationToken cancellationToken = default) =>
    networks.CreateNetworkAsync(new NetworksCreateParameters() { Name = networkName }, cancellationToken);
}