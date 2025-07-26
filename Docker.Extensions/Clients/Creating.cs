namespace Docker.Extensions;

partial class DockerFuncs
{
  public static readonly string DockerHostUri = Environment.GetEnvironmentVariable("DOCKER_HOST") ?? UnixEndpointUri;
  public const string UnixEndpointUri = "unix:///var/run/docker.sock";
  public const string PodmanUnixEndpointUri = "unix:///var/run/podman/podman.sock";
  public const string WindowsEndpointUri = "npipe://./pipe/docker_engine";

  public static IDockerClient CreateDockerClient(Uri uri) => new DockerClientConfiguration(uri).CreateClient();

  public static IDockerClient CreateDockerClient(string uri) => CreateDockerClient(new Uri(uri));
}