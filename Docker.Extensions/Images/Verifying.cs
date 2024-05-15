namespace Docker.Extensions;

partial class DockerFuncs
{
  static bool IsExistingImage<T>(T? image) where T: class => image is not null;
}