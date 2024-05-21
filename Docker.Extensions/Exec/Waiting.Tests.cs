using System.Threading;

namespace Docker.Extensions;

partial class DockerTests
{
  [TestMethod]
  public async Task container_command__wait_for_open_port__port_is_opened()
  {
    const string ImageName = $"mongo:4.2.24";
    const string ContainerName = "storing-mongo";
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    var cancellationToken = cancellationTokenSource.Token;

    using var client = CreateDockerClient();
    await CreateDockerImageAsync(client.Images, ImageName, cancellationToken);
    var containerId = await UseContainerAsync(client.Containers, ImageName, ContainerName, default, cancellationToken);

    await WaitForOpenPort(client.Exec, containerId, 27017, cancellationToken);
  }
}