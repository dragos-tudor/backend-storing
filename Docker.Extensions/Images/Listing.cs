
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task<IList<ImagesListResponse>> ListImagesAsync (IImageOperations images, CancellationToken cancellationToken = default) =>
    images.ListImagesAsync(new ImagesListParameters(){ All = true }, cancellationToken);
}