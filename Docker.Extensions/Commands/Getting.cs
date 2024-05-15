namespace Docker.Extensions;

partial class DockerFuncs
{
  internal static string[] GetKeepRunningContainerCommand() =>
    ["tail","-f", "/dev/null"];

  internal static string[] GetOpenPortVerificationCommand(int port) =>
    ["/bin/bash", "-c", $"</dev/tcp/127.0.0.1/{port}"];
}