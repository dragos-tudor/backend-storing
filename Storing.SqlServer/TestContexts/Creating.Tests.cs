
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static EntitiesContext CreateEntitiesContext (string connString) => new (CreateSqlContextOptions<EntitiesContext>(connString));

  static QueriesContext CreateQueriesContext (string connString) => new (CreateSqlContextOptions<QueriesContext>(connString));

  static TrackingContext CreateTrackingContext () => new (CreateSqlContextOptions<TrackingContext>(""));
}