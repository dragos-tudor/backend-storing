using System.Threading;

namespace Docker.Extensions;

partial class Tasks
{
  static async Task WhileAsync(
    Func<Task<bool>> wait,
    TimeSpan frequency,
    CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (!isSuccessful)
        break;

      await Task.Delay(frequency, cancellationToken);
    }
  }

  static async Task UntilAsync(
    Func<Task<bool>> wait,
    TimeSpan frequency,
    CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (isSuccessful)
        break;

      await Task.Delay(frequency, cancellationToken);
    }
  }

  public static async Task WaitWhileAsync(
    Func<Task<bool>> wait,
    TimeSpan frequency,
    TimeSpan timeout,
    CancellationToken cancellationToken = default)
  {
    var waitTask = WhileAsync(wait, frequency, cancellationToken);
    var timeoutTask = Task.Delay(timeout, cancellationToken);

    var isTimeoutTask = timeoutTask == await Task.WhenAny(waitTask, timeoutTask);
    if (isTimeoutTask)
      throw new TimeoutException();

    await waitTask;
  }

  public static async Task WaitUntilAsync(
    Func<Task<bool>> wait,
    TimeSpan frequency,
    TimeSpan timeout,
    CancellationToken cancellationToken = default)
  {
    var waitTask = UntilAsync(wait, frequency, cancellationToken);
    var timeoutTask = Task.Delay(timeout, cancellationToken);

    var isTimeoutTask = timeoutTask == await Task.WhenAny(waitTask, timeoutTask);
    if (isTimeoutTask)
      throw new TimeoutException();

    await waitTask;
  }

}

