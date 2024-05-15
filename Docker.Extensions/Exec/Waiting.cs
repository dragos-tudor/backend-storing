
namespace Docker.Extensions;

partial class DockerFuncs
{
  public async static Task WaitForOpenPort (
    IExecOperations exec,
    string containerId,
    int port,
    TimeSpan timeout)
  {
    var retryAfter = TimeSpan.FromMicroseconds(500);
    var openPortVerificationCommand = GetOpenPortVerificationCommand(port);

    await WaitUntilAsync(async () =>
      await ExecContainerCommandAsync(exec, containerId, openPortVerificationCommand) == 0,
      retryAfter,
      timeout
    );
  }
}

