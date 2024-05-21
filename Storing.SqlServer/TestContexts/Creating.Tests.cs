
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static EntitiesContext CreateEntitiesContext () => new (CreateDbContextOptions<EntitiesContext>("entities"));

  static QueriesContext CreateQueriesContext () => new (CreateDbContextOptions<QueriesContext>("queries"));

  static TrackingContext CreateTrackingContext () => new (CreateDbContextOptions<TrackingContext>("tracking"));
}