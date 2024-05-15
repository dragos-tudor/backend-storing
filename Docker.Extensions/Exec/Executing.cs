using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<long> ExecContainerCommandAsync(
    IExecOperations exec,
    string containerId,
    string[] command,
    CancellationToken cancellationToken = default)
  {
    var createParameters = new ContainerExecCreateParameters
    {
      Cmd = command,
      AttachStdout = true,
      AttachStderr = true
    };
    var createResponse = await exec.ExecCreateContainerAsync(containerId, createParameters, cancellationToken);
    await ReadOutputAndErrorAsync(exec, createResponse.ID, cancellationToken);

    var inspectResponse = await exec.InspectContainerExecAsync(createResponse.ID, cancellationToken);
    return inspectResponse.ExitCode;
  }

}