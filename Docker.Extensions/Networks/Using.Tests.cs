
namespace Docker.Extensions;

partial class DockerTests
{
  [TestMethod]
  public async Task new_network__use_network__network_created()
  {
    using var client = CreateDockerClient();
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    const string networkName = "storing-network-test";
    var cancellationToken = cancellationTokenSource.Token;

    var networkList = await ListNetworksAsync(client.Networks, cancellationToken);
    var networkListItem = GetNetwork(networkList, networkName);
    if (ExistNetwork(networkListItem)) await client.Networks.DeleteNetworkAsync(networkName, cancellationToken);

    await UseNetworkAsync(client.Networks, networkName, cancellationToken);
    var networkList2 = await ListNetworksAsync(client.Networks, cancellationToken);
    Assert.IsNotNull(GetNetwork(networkList2, networkName));
  }
}