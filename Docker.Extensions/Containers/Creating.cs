
namespace Docker.Extensions;

partial class DockerFuncs
{
  static async Task<string> CreateContainerAsync (
    IContainerOperations containers,
    string imageName,
    string containerName,
    Action<CreateContainerParameters>? setCreateContainerParameters = default,
    CancellationToken cancellationToken = default)
  {
    var createContainerParameters = new CreateContainerParameters() { Image = imageName, Name = containerName };
    if (setCreateContainerParameters is not null) setCreateContainerParameters(createContainerParameters);

    var containerCreated = await containers.CreateContainerAsync(createContainerParameters, cancellationToken);
    return containerCreated.ID;
  }
}