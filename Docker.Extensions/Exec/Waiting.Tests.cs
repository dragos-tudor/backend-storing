
namespace Docker.Extensions;

partial class DockerTests
{
  [TestMethod]
  public async Task container_command__wait_for_open_port__port_is_opened()
  {
    const string imageName = $"nginx:1.26-alpine-slim";
    const string containerName = "storing-nginx";

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));
    var cancellationToken = cancellationTokenSource.Token;

    using var client = CreateDockerClient();
    Action<CreateContainerParameters> setCreateContainerParameters = (@params) => {
      @params.HostConfig = new HostConfig() { NetworkMode = "storing-network" };
    };
    var container = await UseContainerAsync(client, imageName, containerName, setCreateContainerParameters, cancellationToken);

    await WaitForOpenPortWtihNetCat(client.Exec, container.ID, 80, cancellationToken);
  }
}