using static Storing.SqlServer.SqlOptions;
using static Storing.SqlServer.SqlContexts;
using static Storing.SqlServer.TestServers;

namespace Storing.SqlServer;

static partial class TestContexts
{
  internal const string AdminName = "sa";
  internal const string AdminPassword = "admin.P@ssw0rd";

  static async Task<DbContextOptions<TContext>> CreateDbContextOptions<TContext> (string dbName) where TContext: DbContext =>
    IsInMemoryContext()?
      CreateInMemoryContextOptions<TContext>(dbName):
      CreateSqlContextOptions<TContext>(
        CreateDbConnectionString(
          dbName,
          await ServerIpAddress.Value,
          AdminName,
          AdminPassword));

  internal static async Task<EntitiesContext> CreateEntitiesContext (bool? shouldEnsureDatabase = false) =>
    new (await CreateDbContextOptions<EntitiesContext>("entities"), shouldEnsureDatabase);

  internal static async Task<QueriesContext> CreateQueriesContext () =>
    new (await CreateDbContextOptions<QueriesContext>("queries"));

  internal static async Task<TrackingContext> CreateTrackingContext () =>
    new (await CreateDbContextOptions<TrackingContext>("tracking"));
}