namespace Docker.Extensions;

partial class DockerFuncs
{
  internal static string FormatDockerMessage (JSONMessage message) =>
    $"{message.Status} {message.ProgressMessage}";

  internal static string FormatDockerErrorMessage (JSONMessage message) =>
    $"{message.Error} {message.ErrorMessage}";
}