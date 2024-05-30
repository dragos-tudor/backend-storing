
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static string GetEdpointIpAddress (EndpointSettings endpointSettings) => endpointSettings.IPAddress;

  public static NetworkResponse? GetNetwork (IEnumerable<NetworkResponse> networkResponses, string networkName) =>
    networkResponses.FirstOrDefault(network => network.Name == networkName);

  public static EndpointSettings GetNetworkEndpoints (NetworkSettings networkSettings, string networkName) => networkSettings.Networks[networkName];

  public static string GetNetworkIpAddress (NetworkSettings networkSettings) => networkSettings.IPAddress;
}