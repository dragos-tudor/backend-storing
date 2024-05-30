
namespace Docker.Extensions;

partial class DockerFuncs
{
  internal static bool ExistNetwork<T>(T? network) where T: class => network is not null;
}