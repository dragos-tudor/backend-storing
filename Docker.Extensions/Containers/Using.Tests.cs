using System.Threading;
using Xunit;
using static Docker.Extensions.Clients;
using static Docker.Extensions.Commands;
using static Docker.Extensions.Containers;
using static Docker.Extensions.Images;

namespace Docker.Extensions;

public class ContainersTests
{
  const string imageName = "alpine:3.18.5";
  const string containerName = "test-container";

  [Fact]
  public async Task different_container_state__use_container__running_container()
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    await CreateDockerImageAsync(client.Images, imageName, cts.Token);

    var containerId = await UseContainerAsync(client.Containers, imageName, containerName,
      (@params) => @params.Entrypoint = GetKeepRunningContainerCommand(), cts.Token);

    var state1 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.True(state1!.Running);


    await client.Containers.PauseContainerAsync(containerId, cts.Token);
    await UseContainerAsync(client.Containers, imageName, containerName, default, cts.Token);

    var state3 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.True(state3!.Running);


    await client.Containers.StopContainerAsync(containerId, new(), cts.Token);
    await UseContainerAsync(client.Containers, imageName, containerName, default, cts.Token);

    var state2 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.True(state2!.Running);


    await client.Containers.KillContainerAsync(containerId, new(), cts.Token);
    await UseContainerAsync(client.Containers, imageName, containerName, default, cts.Token);

    var state4 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.True(state4!.Running);


    await UseContainerAsync(client.Containers, imageName, containerName, default, cts.Token);

    var state6 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.True(state6!.Running);
  }

}