
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static Task WaitForCommand (IExecOperations exec, string containerId, string[] command, CancellationToken cancellationToken = default) =>
    WaitUntilAsync(
      async () => await ExecContainerCommandAsync(exec, containerId, command, cancellationToken) == 0,
      TimeSpan.FromMilliseconds(1000),
      cancellationToken
    );

  public static Task WaitForOpenPortWtihBash (IExecOperations exec, string containerId, int port, CancellationToken cancellationToken = default) =>
    WaitForCommand (exec, containerId, GetVerifyOpenPortBashCommand(port), cancellationToken);

  public static Task WaitForOpenPortWtihNetCat (IExecOperations exec, string containerId, int port, CancellationToken cancellationToken = default) =>
    WaitForCommand (exec, containerId, GetVerifyOpenPortNetCatCommand(port), cancellationToken);
}

