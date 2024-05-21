
namespace Docker.Extensions;

partial class DockerFuncs
{
  static void LogException(Exception ex) => Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
}