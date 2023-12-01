using System.Threading;

namespace Docker.Extensions;

static partial class Containers
{
  static async Task<string> CreateContainerAsync(
    IContainerOperations containers,
    CreateContainerParameters createContainerParameters,
    CancellationToken cancellationToken = default)
  {
    var containerCreated = await containers.CreateContainerAsync(createContainerParameters, cancellationToken);
    return containerCreated.ID;
  }
}