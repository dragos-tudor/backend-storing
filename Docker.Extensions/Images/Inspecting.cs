using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<ImageInspectResponse?> InspectImageAsync(
    IImageOperations images,
    string imageName,
    CancellationToken cancellationToken = default)
  {
    try {
      return await images.InspectImageAsync(imageName, cancellationToken);
    }
    catch(DockerImageNotFoundException) {
      return default;
    }
  }
}