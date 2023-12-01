using System.Threading;

namespace Docker.Extensions;

public static partial class Images
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