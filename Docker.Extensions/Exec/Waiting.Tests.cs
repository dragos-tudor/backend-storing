
namespace Docker.Extensions;

partial class DockerTests
{
  [TestMethod]
  public async Task container_command__wait_for_open_port__port_is_opened()
  {
    const string imageName = $"docker.io/libraries/nginx:latest";
    const string containerName = "storing-nginx";

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));
    var cancellationToken = cancellationTokenSource.Token;

    using var client = CreateDockerClient(PodmanUnixEndpointUri);
    Action<CreateContainerParameters> setCreateContainerParameters = (@params) => {
      @params.HostConfig = new HostConfig() { NetworkMode = "host" };
    };
    var container = await UseContainerAsync(client, imageName, containerName, setCreateContainerParameters, cancellationToken);

    await WaitForOpenPortWtihNetCat(client.Exec, container.ID, 80, cancellationToken);
  }
}
