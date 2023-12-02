using System.Threading;

namespace Docker.Extensions;

static partial class Containers
{
  static async Task<string> CreateContainerAsync(
    IContainerOperations containers,
    string imageName,
    string containerName,
    Action<CreateContainerParameters>? setContainerParams = default,
    CancellationToken cancellationToken = default)
  {
    var createContainerParams = new CreateContainerParameters() { Image = imageName, Name = containerName };
    setContainerParams?.Invoke(createContainerParams);

    var containerCreated = await containers.CreateContainerAsync(createContainerParams, cancellationToken);
    return containerCreated.ID;
  }
}