using static Docker.Extensions.Commands;
using static Docker.Extensions.Tasks;

namespace Docker.Extensions;

partial class Exec
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

