
namespace Docker.Extensions;

partial class DockerFuncs
{
  static bool ExistImage<T>(T? image) where T: class => image is not null;
}