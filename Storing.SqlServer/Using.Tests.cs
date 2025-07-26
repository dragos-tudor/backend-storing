global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;

namespace Storing.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly string QueriesConnString = CreateSqlConnectionString("queries", "sa", "P@ssw0rd!", "127.0.0.1");
  static readonly string EntitiesConnString = CreateSqlConnectionString("entities", "sa", "P@ssw0rd!", "127.0.0.1");
  static readonly string TrackingConnString = CreateSqlConnectionString("tracking", "sa", "P@ssw0rd!", "127.0.0.1");

  static void InitializeSqlDatabase<TContext>(TContext dbContext) where TContext : DbContext
  {
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
  }

  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    using var entitiesContext = CreateEntitiesContext();
    InitializeSqlDatabase(entitiesContext);

    using var queriesContext = CreateQueriesContext();
    InitializeSqlDatabase(queriesContext);

       using var trackingContext = CreateTrackingContext();
    InitializeSqlDatabase(trackingContext);
  }


}