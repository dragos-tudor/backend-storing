#pragma warning disable CA2201
#pragma warning disable CS0162

namespace Docker.Extensions;

partial class DockerTests
{
  [TestMethod]
  public void returning_async_func_and_sync_caller__run_synchronously__caller_wait_func_to_end ()
  {
    Assert.IsTrue(RunSynchronously(() => Task.FromResult(true)));
    Assert.IsTrue(RunSynchronously(async () => { await Task.Delay(0); return true; }));
  }

  [TestMethod]
  public void non_returning_async_func_and_sync_caller__run_synchronously__caller_wait_func_to_end ()
  {
    bool[] results = [false, false];
    RunSynchronously(() => { results[0] = true; return Task.CompletedTask; });
    RunSynchronously(async () => { await Task.Delay(0); results[1] = true; });
    AreEqual(results, [true, true]);
  }

  [TestMethod]
  public void throwing_exception_returning_async_func_and_sync_caller__run_synchronously__throw_aggregate_with_inner_exception ()
  {
    try { RunSynchronously(() => { if(true) throw new Exception("abc"); return Task.FromResult(true); }); }
    catch (AggregateException ex) { StringAssert.Contains(ex.InnerException!.Message, "abc", StringComparison.InvariantCulture); };
  }

  [TestMethod]
  public void throwing_exception_non_returning_async_func_and_sync_caller__run_synchronously__throw_aggregate_with_inner_exception ()
  {
    try { RunSynchronously(() => { throw new Exception("abc"); }); }
    catch (AggregateException ex) { StringAssert.Contains(ex.InnerException!.Message, "abc", StringComparison.InvariantCulture); };
  }
}