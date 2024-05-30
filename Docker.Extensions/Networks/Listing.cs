
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task<IList<NetworkResponse>> ListNetworksAsync (INetworkOperations networks, CancellationToken cancellationToken = default) =>
    networks.ListNetworksAsync(default, cancellationToken);
}