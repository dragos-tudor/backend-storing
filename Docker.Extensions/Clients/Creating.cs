namespace Docker.Extensions;
#pragma warning disable CA2000

partial class DockerFuncs
{
  const string HttpEndpointUri = "tcp://172.17.0.1:2375";
  public static readonly string DockerHostUri = Environment.GetEnvironmentVariable("DOCKER_HOST") ?? HttpEndpointUri;
  public const string UnixEndpointUri = "unix:///var/run/docker.sock";
  public const string WindowsEndpointUri = "npipe://./pipe/docker_engine";

  public static IDockerClient CreateDockerClient(Uri? uri = default) =>
    new DockerClientConfiguration(uri ?? new Uri(DockerHostUri)).CreateClient();
}