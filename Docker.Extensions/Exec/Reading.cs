
namespace Docker.Extensions;

partial class DockerFuncs
{
  public static async Task<(string Output, string Error)> ReadOutputAndErrorAsync(
    IExecOperations exec,
    string execId,
    CancellationToken cancellationToken = default)
  {
    using var stdOutAndErrStream = await exec.StartAndAttachContainerExecAsync(execId, false, cancellationToken);
    return await stdOutAndErrStream.ReadOutputToEndAsync(cancellationToken);
  }
}