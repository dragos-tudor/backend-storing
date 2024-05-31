namespace Docker.Extensions;
#pragma warning disable CA2000

partial class DockerFuncs
{
  public static readonly string HttpEndpointUri = $"http://{Environment.GetEnvironmentVariable("DOCKER_HOST_GATEWAY")}:{Environment.GetEnvironmentVariable("DOCKER_HOST_PORT")}";
  public const string UnixEndpointUri = "unix:///var/run/docker.sock";
  public const string WindowsEndpointUri = "npipe://./pipe/docker_engine";

  public static IDockerClient CreateDockerClient(Uri? uri = default) =>
    new DockerClientConfiguration(uri ?? new Uri(HttpEndpointUri))
      .CreateClient();
}