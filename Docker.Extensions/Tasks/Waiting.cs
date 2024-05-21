using System.Threading;

namespace Docker.Extensions;

partial class DockerFuncs
{
  internal static async Task WaitWhileAsync(
    Func<Task<bool>> wait,
    TimeSpan retryAfter,
    CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (!isSuccessful)
        break;

      await Task.Delay(retryAfter, cancellationToken);
    }
  }

  internal static async Task WaitUntilAsync(
    Func<Task<bool>> wait,
    TimeSpan retryAfter,
    CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (isSuccessful)
        break;

      await Task.Delay(retryAfter, cancellationToken);
    }
  }

}

