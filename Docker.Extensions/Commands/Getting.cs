namespace Docker.Extensions;

partial class DockerFuncs
{
  public static string[] GetKeepRunningContainerCommand () =>
    ["tail","-f", "/dev/null"];

  public static string[] GetVerifyOpenPortBashCommand (int port) =>
    ["/bin/bash", "-c", $"</dev/tcp/127.0.0.1/{port}"];

  public static string[] GetVerifyOpenPortNetCatCommand (int port) =>
    ["/bin/sh", "-c", $"nc -z 127.0.0.1 {port}"];
}