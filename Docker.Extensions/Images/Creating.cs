using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<bool> CreateDockerImageAsync (
    IImageOperations images,
    string imageName,
    CancellationToken cancellationToken = default)
  {
    var image = await InspectImageAsync(images, imageName, cancellationToken);
    if(IsExistingImage(image)) return false;

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