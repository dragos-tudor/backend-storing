#pragma warning disable CA2025

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;

namespace Storing.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly string QueriesConnString = CreateSqlConnectionString("queries", "sa", "P@ssw0rd!", "127.0.0.1");
  static readonly string EntitiesConnString = CreateSqlConnectionString("entities", "sa", "P@ssw0rd!", "127.0.0.1");
  static readonly string TrackingConnString = CreateSqlConnectionString("tracking", "sa", "P@ssw0rd!", "127.0.0.1");

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