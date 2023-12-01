using System.Threading;
using Xunit;
using static Docker.Extensions.Clients;
using static Docker.Extensions.Containers;
using static Docker.Extensions.Images;

namespace Docker.Extensions;

public sealed partial class ContainersTests
{
  const string containerName = "storing-mongo";
  const string imageName = "mongo:4.2.24";

  [Fact]
  public async Task mongo_container__start_container__container_started()
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(300_000);
    await CreateDockerImageAsync(client.Images, imageName, cts.Token);

    var containerId = await StartContainerAsync(client.Containers, imageName, containerName, default, cts.Token);
    var container = await InspectContainerAsync(client.Containers, containerId, cts.Token);

    Assert.True(container!.State.Running);
  }

}