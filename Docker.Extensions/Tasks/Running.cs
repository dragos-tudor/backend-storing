#pragma warning disable CA1031

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static T RunSynchronously<T> (Func<Task<T>> func)
  {
    using var autoResetEvent = new AutoResetEvent(false);
    T result = default!;

    var task = async () => {
      try { result = await func(); }
      catch (Exception ex) { LogException(ex); }
      finally { autoResetEvent.Set(); }
    };
    var thread = new Thread(() => task());
    thread.Start();

    autoResetEvent.WaitOne();
    return result;
  }
}