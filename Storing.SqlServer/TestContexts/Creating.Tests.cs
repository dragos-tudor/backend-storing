
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static EntitiesContext CreateEntitiesContext (string? connString = default) => new (CreateSqlContextOptions<EntitiesContext>(connString ?? EntitiesConnString));

  static QueriesContext CreateQueriesContext (string? connString = default) => new (CreateSqlContextOptions<QueriesContext>(connString ?? QueriesConnString));

  static TrackingContext CreateTrackingContext (string? connString = default) => new (CreateSqlContextOptions<TrackingContext>(connString ?? TrackingConnString));
}