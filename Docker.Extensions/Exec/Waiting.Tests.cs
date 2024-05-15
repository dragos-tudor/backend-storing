using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docker.Extensions;

[TestClass]
public class ExecTests
{
  [TestMethod]
  public async Task container_command__wait_for_open_port__port_is_opened()
  {
    const string ImageName = $"mongo:4.2.24";
    const string ContainerName = "storing-mongo";

    using var client = CreateDockerClient();
    await CreateDockerImageAsync(client.Images, ImageName);
    var containerId = await UseContainerAsync(client.Containers, ImageName, ContainerName);

    await WaitForOpenPort(client.Exec, containerId, 27017, TimeSpan.FromSeconds(10));
  }
}