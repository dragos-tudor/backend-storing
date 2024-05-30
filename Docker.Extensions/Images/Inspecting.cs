
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task<ImageInspectResponse?> InspectImageAsync (IImageOperations images, string imageName, CancellationToken cancellationToken = default) =>
    images.InspectImageAsync(imageName, cancellationToken);
}