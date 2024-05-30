
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<string> UseNetworkAsync (string networkName, CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    var networks = client.Networks;

    var networkResponses = await ListNetworksAsync(networks, cancellationToken);
    var networkResponse = FindNetworkByName(networkResponses, networkName);
    if (ExistNetwork(networkResponse)) return networkResponse!.ID;

    var networkCreateResponse = await CreateNetworkAsync(networks, networkName, cancellationToken);
    return networkCreateResponse.ID;
  }
}