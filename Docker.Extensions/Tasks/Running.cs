#pragma warning disable CA1031

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static T RunSynchronously<T> (Func<Task<T>> func)
  {
    using var manualResetEvent = new ManualResetEventSlim(false);
    Exception? exception = default;
    T result = default!;

    ThreadPool.QueueUserWorkItem(async (_) => {
      try { result = await func(); }
      catch (Exception ex) { exception = ex; }
      finally { manualResetEvent.Set(); }
    });

    manualResetEvent.Wait();

    // preserve original stack [no rethrow]
    if(exception is not null) throw new AggregateException(exception);
    return result;
  }

  public static bool RunSynchronously (Func<Task> func)
  {
    using var manualResetEvent = new ManualResetEventSlim(false);
    Exception? exception = default;

    ThreadPool.QueueUserWorkItem(async (_) => {
      try { await func(); }
      catch (Exception ex) { exception = ex; }
      finally { manualResetEvent.Set(); }
    });

    manualResetEvent.Wait();

    // preserve original stack [no rethrow]
    if(exception is not null) throw new AggregateException(exception);
    return true;
  }
}