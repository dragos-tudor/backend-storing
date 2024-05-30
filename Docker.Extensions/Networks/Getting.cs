
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static EndpointSettings GetEndpointSettings (NetworkSettings networkSettings, string networkName) =>
    networkSettings.Networks[networkName];

  public static NetworkResponse? FindNetworkByName (IEnumerable<NetworkResponse> networkResponses, string networkName) =>
    networkResponses.FirstOrDefault(networkResponse => networkResponse.Name == networkName);
}