
using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  public async static Task WaitForOpenPort (
    IExecOperations exec,
    string containerId,
    int port,
    CancellationToken cancellationToken = default)
  {
    var retryAfter = TimeSpan.FromMilliseconds(1000);
    var verifyOpenPortCommand = GetVerifyOpenPortCommand(port);

    await WaitUntilAsync(async () =>
      await ExecContainerCommandAsync(exec, containerId, verifyOpenPortCommand, cancellationToken) == 0,
      retryAfter,
      cancellationToken
    );
  }
}

