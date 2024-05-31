
namespace Docker.Extensions;

partial class DockerTests
{
  [TestMethod]
  public async Task different_container_states__start_container__running_container()
  {
    const string imageName = "alpine:3.18.5";
    const string containerName = "storing-alpine";

    using var client = CreateDockerClient();
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    var cancellationToken = cancellationTokenSource.Token;

    Action<CreateContainerParameters> setCreateContainerParameters = (@params) => {
      @params.Entrypoint = GetKeepRunningContainerCommand();
      @params.HostConfig = new HostConfig() { NetworkMode = "storing-network" };
    };
    var container = await UseContainerAsync(client, imageName, containerName, setCreateContainerParameters, cancellationToken);
    var container1 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    Assert.IsTrue(container1.State.Running);

    await client.Containers.PauseContainerAsync(container.ID, cancellationToken);
    var container2 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    await StartContainerAsync(client.Containers, container2, cancellationToken);
    var container3 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    Assert.IsTrue(container3.State.Running);

    await client.Containers.StopContainerAsync(container.ID, new(), cancellationToken);
    var container4 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    await StartContainerAsync(client.Containers, container4, cancellationToken);
    var container5 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    Assert.IsTrue(container5.State.Running);

    await client.Containers.KillContainerAsync(container.ID, new(), cancellationToken);
    var container6 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    await StartContainerAsync(client.Containers, container6, cancellationToken);
    var container7 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    Assert.IsTrue(container7.State.Running);

    await StartContainerAsync(client.Containers, container7, cancellationToken);
    var container8 = await InspectContainerAsync(client.Containers, container.ID, cancellationToken);
    Assert.IsTrue(container8.State.Running);
  }

}