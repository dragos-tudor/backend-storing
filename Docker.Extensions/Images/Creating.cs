
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<bool> CreateDockerImageAsync (
    IImageOperations images,
    string imageName,
    CancellationToken cancellationToken = default)
  {
    var imageList = await ListImagesAsync(images, cancellationToken);
    var imageListItem = GetImage(imageList, imageName);
    if(ExistImage(imageListItem)) return false;

    var errorLogger = GetDockerErrorLogger();
    var createImageProgress = new Progress<JSONMessage>(errorLogger);
    var createImageParameters = new ImagesCreateParameters{
      FromImage = GetFromImage(imageName),
      Tag = GetImageTag(imageName) ?? "latest",
    };

    Console.WriteLine($"Creating image {imageName}");
    await images.CreateImageAsync(
      createImageParameters, null,
      createImageProgress,
      cancellationToken);
    Console.WriteLine($"Created image {imageName}");

    return true;
  }

}