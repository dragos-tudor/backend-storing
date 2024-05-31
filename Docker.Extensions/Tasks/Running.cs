#pragma warning disable CA1031

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static T RunSynchronously<T> (Func<Task<T>> func)
  {
    using var autoResetEvent = new AutoResetEvent(false);
    Exception? exception = default;

    T result = default!;

    var task = async () => {
      try { result = await func(); }
      catch (Exception ex) { exception = ex; }
      finally { autoResetEvent.Set(); }
    };
    var thread = new Thread(() => task());
    thread.Start();

    autoResetEvent.WaitOne();

    if(exception is not null) throw new AggregateException(exception);
    return result;
  }
}