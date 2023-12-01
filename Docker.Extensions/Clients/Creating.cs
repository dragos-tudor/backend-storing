namespace Docker.Extensions;

public static partial class Clients
{
  public const string UnixEndpointUri = "unix:///var/run/docker.sock";
  public const string WindowsEndpointUri = "npipe://./pipe/docker_engine";

  public static DockerClient CreateDockerClient(string uriString = UnixEndpointUri) =>
    new DockerClientConfiguration(new Uri(uriString))
      .CreateClient();
}