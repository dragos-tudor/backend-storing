
namespace Docker.Extensions;

partial class DockerFuncs
{
  static bool ExistNetwork (NetworkResponse? networkResponse) => networkResponse is not null;
}