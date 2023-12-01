using System.Threading;
using static Docker.Extensions.Commands;
using static Docker.Extensions.Tasks;

namespace Docker.Extensions;

partial class Exec
{
  public static Task WaitForOpenPort(
    IExecOperations exec,
    string containerId,
    int port,
    TimeSpan timeout,
    CancellationToken cancellationToken = default) =>
      WaitUntilAsync(
        async () => await ExecContainerCommandAsync(exec, containerId, GetOpenPortVerificationCommand(port), cancellationToken) == 0,
        TimeSpan.FromMicroseconds(500),
        timeout,
        cancellationToken);
}

