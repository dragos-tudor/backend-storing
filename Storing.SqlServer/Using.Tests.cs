#pragma warning disable CA2025

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;
using System.Threading;

namespace Storing.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly string QueriesConnString = CreateSqlConnectionString("sql", "sa", GetSqlServerAdminPassword()!, "queries", TimeSpan.FromSeconds(5));
  static readonly string EntitiesConnString = CreateSqlConnectionString("sql", "sa", GetSqlServerAdminPassword()!, "entities", TimeSpan.FromSeconds(5));
  static readonly string TrackingConnString = CreateSqlConnectionString("sql", "sa", GetSqlServerAdminPassword()!, "tracking", TimeSpan.FromSeconds(5));
  readonly TestContext TestContext;

  public SqlServerTests(TestContext testContext) => TestContext = testContext;

  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    using var entitiesContext = CreateEntitiesContext();
    using var queriesContext = CreateQueriesContext();
    using var trackingContext = CreateTrackingContext();

    Task.WaitAll(
      InitializeSqlDatabase(entitiesContext),
      InitializeSqlDatabase(queriesContext),
      InitializeSqlDatabase(trackingContext)
    );
  }

}
