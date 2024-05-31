global using System.Threading;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;

namespace Storing.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      InitializeSqlServer(
        "sa", "admin.P@ssw0rd",
        "mcr.microsoft.com/mssql/server:2019-latest", "storing-sql",
        "storing-network", 1433,
        cancellationToken));
  }
}