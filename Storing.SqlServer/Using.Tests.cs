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
    StartSqlServer();
    CleanEntitiesDatabase();
    CleanQueriesDatabase();
  }
}