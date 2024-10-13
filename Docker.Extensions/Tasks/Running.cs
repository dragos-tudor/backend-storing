#pragma warning disable CA1031

namespace Docker.Extensions;

partial class DockerFuncs
{
  public static T RunSynchronously<T> (Func<Task<T>> func)
  {
    var taskCompletionSource = new TaskCompletionSource<T>();
    var task = taskCompletionSource.Task;

    ThreadPool.QueueUserWorkItem(async (_) => {
      try { taskCompletionSource.SetResult(await func()); }
      catch (Exception ex) { taskCompletionSource.SetException(ex); }
    });

    return task.GetAwaiter().GetResult();
  }

  public static bool RunSynchronously (Func<Task> func)
  {
    var taskCompletionSource = new TaskCompletionSource<bool>();
    var task = taskCompletionSource.Task;

    ThreadPool.QueueUserWorkItem(async (_) => {
      try { await func(); taskCompletionSource.SetResult(true); }
      catch (Exception ex) { taskCompletionSource.SetException(ex); }
    });

    return task.GetAwaiter().GetResult();
  }
}
