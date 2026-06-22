#pragma warning disable CA2025

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;

namespace Storing.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly string QueriesConnString = CreateSqlConnectionString("127.0.0.1", "sa", "P@ssw0rd!", "queries");
  static readonly string EntitiesConnString = CreateSqlConnectionString("127.0.0.1", "sa", "P@ssw0rd!", "entities");
  static readonly string TrackingConnString = CreateSqlConnectionString("127.0.0.1", "sa", "P@ssw0rd!", "tracking");

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