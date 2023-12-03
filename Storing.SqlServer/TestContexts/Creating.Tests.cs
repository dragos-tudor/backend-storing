using static Storing.SqlServer.SqlOptions;
using static Storing.SqlServer.SqlContexts;

namespace Storing.SqlServer;

static partial class TestContexts
{
  static async Task<DbContextOptions<TContext>> CreateDbContextOptions<TContext> (string dbName) where TContext: DbContext =>
    IsInMemoryContext()?
      CreateInMemoryContextOptions<TContext>(dbName):
      CreateSqlContextOptions<TContext>(await CreateDbConnectionString(dbName));

  internal static async Task<EntitiesContext> CreateEntitiesContext (bool? shouldEnsureDatabase = false) =>
    new (await CreateDbContextOptions<EntitiesContext>("entities"), shouldEnsureDatabase);

  internal static async Task<QueriesContext> CreateQueriesContext () =>
    new (await CreateDbContextOptions<QueriesContext>("queries"));

  internal static async Task<TrackingContext> CreateTrackingContext () =>
    new (await CreateDbContextOptions<TrackingContext>("tracking"));

}