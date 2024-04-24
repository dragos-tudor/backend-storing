using System.Threading;

namespace Docker.Extensions;

static partial class Tasks
{
  internal static async Task WaitWhileAsync(
    Func<Task<bool>> wait,
    TimeSpan retryAfter,
    TimeSpan timeout)
  {
    using var cts = new CancellationTokenSource(timeout);
    while (true)
    {
      var isSuccessful = await wait.Invoke();
      if (!isSuccessful)
        break;

      await Task.Delay(retryAfter, cts.Token);
    }
  }

  internal static async Task WaitUntilAsync(
    Func<Task<bool>> wait,
    TimeSpan retryAfter,
    TimeSpan timeout)
  {
    using var cts = new CancellationTokenSource(timeout);
    while (true)
    {
      var isSuccessful = await wait.Invoke();
      if (isSuccessful)
        break;

      await Task.Delay(retryAfter, cts.Token);
    }
  }

}

