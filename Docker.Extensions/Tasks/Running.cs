#pragma warning disable CA1031

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static T RunSynchronously<T> (Func<Task<T>> func)
  {
    using var manualResetEvent = new ManualResetEventSlim(false);
    Exception? exception = default;
    T result = default!;

    var task = async () => {
      try { result = await func(); }
      catch (Exception ex) { exception = ex; }
      finally { manualResetEvent.Set(); }
    };
    var thread = new Thread(() => task());
    thread.Start();

    manualResetEvent.Wait();

    if(exception is not null) throw new AggregateException(exception);
    return result;
  }
}