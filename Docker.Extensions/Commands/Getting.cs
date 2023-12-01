namespace Docker.Extensions;

public static partial class Commands
{
  public static string[] GetOpenPortVerificationCommand(int port) =>
    ["/bin/bash", "-c", $"</dev/tcp/127.0.0.1/{port}"];
}