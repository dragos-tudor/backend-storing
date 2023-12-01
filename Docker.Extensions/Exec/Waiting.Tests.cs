using System.Threading;
using Xunit;
using static Docker.Extensions.Clients;
using static Docker.Extensions.Containers;
using static Docker.Extensions.Exec;
using static Docker.Extensions.Images;

namespace Docker.Extensions;

public class ExecTests
{
  const string mongoContainerName = "storing-mongo";
  const string mongoImage = $"mongo:4.2.24";

  [Fact]
  public async Task container_command__wait_for_open_port__port_is_opened()
  {
    using var client = CreateDockerClient();
    await CreateDockerImageAsync(client.Images, mongoImage);
    var containerId = await StartContainerAsync(client.Containers, mongoImage, mongoContainerName);

    await WaitForOpenPort(client.Exec, containerId, 27017, TimeSpan.FromSeconds(10));

  }
}