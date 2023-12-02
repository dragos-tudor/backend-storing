using System.Threading;
using static Docker.Extensions.Messages;

namespace Docker.Extensions;

public static partial class Images
{
  public static async Task<bool> CreateDockerImageAsync (
    IImageOperations images,
    string imageName,
    CancellationToken cancellationToken = default)
  {
    var image = await InspectImageAsync(images, imageName, cancellationToken);
    if(IsExistingImage(image)) return false;

    await images.CreateImageAsync(
      new ImagesCreateParameters{
        FromImage = GetFromImage(imageName),
        Tag = GetImageTag(imageName) ?? "latest",
      },
      null,
      new Progress<JSONMessage>(LogMessages(10)),
      cancellationToken);

    return true;
  }

}