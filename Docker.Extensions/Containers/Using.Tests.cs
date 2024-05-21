using System.Threading;

namespace Docker.Extensions;

partial class DockerTests
{
  const string ImageName = "alpine:3.18.5";
  const string ContainerName = "test-container";

  [TestMethod]
  public async Task different_container_state__use_container__running_container()
  {
    using var client = CreateDockerClient();
    using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    await CreateDockerImageAsync(client.Images, ImageName, cts.Token);

    var containerId = await UseContainerAsync(client.Containers, ImageName, ContainerName,
      (@params) => @params.Entrypoint = GetKeepRunningContainerCommand(), cts.Token);

    var state1 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.IsTrue(state1!.Running);


    await client.Containers.PauseContainerAsync(containerId, cts.Token);
    await UseContainerAsync(client.Containers, ImageName, ContainerName, default, cts.Token);

    var state3 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.IsTrue(state3!.Running);


    await client.Containers.StopContainerAsync(containerId, new(), cts.Token);
    await UseContainerAsync(client.Containers, ImageName, ContainerName, default, cts.Token);

    var state2 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.IsTrue(state2!.Running);


    await client.Containers.KillContainerAsync(containerId, new(), cts.Token);
    await UseContainerAsync(client.Containers, ImageName, ContainerName, default, cts.Token);

    var state4 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.IsTrue(state4!.Running);


    await UseContainerAsync(client.Containers, ImageName, ContainerName, default, cts.Token);

    var state6 = await GetContainerStateAsync(client.Containers, containerId, cts.Token);
    Assert.IsTrue(state6!.Running);
  }

}