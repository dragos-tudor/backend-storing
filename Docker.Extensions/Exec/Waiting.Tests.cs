using Xunit;
using static Docker.Extensions.Clients;
using static Docker.Extensions.Containers;
using static Docker.Extensions.Exec;
using static Docker.Extensions.Images;

namespace Docker.Extensions;

public class ExecTests
{
  [Fact]
  public async Task container_command__wait_for_open_port__port_is_opened()
  {
    const string imageName = $"mongo:4.2.24";
    const string containerName = "storing-mongo";

    using var client = CreateDockerClient();
    await CreateDockerImageAsync(client.Images, imageName);
    var containerId = await UseContainerAsync(client.Containers, imageName, containerName);

    await WaitForOpenPort(client.Exec, containerId, 27017, TimeSpan.FromSeconds(10));

  }
}