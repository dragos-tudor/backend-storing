namespace Docker.Extensions;

static partial class Images
{
  static bool IsExistingImage<T>(T? image) where T: class => image is not null;
}