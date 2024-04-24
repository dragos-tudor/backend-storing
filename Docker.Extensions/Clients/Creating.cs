namespace Docker.Extensions;
#pragma warning disable CA2000

public static partial class Clients
{
  public const string UnixEndpointUri = "unix:///var/run/docker.sock";
  public const string WindowsEndpointUri = "npipe://./pipe/docker_engine";

  public static DockerClient CreateDockerClient(Uri? uri = default) =>
    new DockerClientConfiguration(uri ?? new Uri(UnixEndpointUri))
      .CreateClient();
}