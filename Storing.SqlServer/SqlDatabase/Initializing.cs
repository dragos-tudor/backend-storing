
namespace Storing.SqlServer;

partial class SqlServerTests
{
  public static async Task InitializeSqlDatabase<TContext>(TContext dbContext) where TContext : DbContext
  {
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();
    await dbContext.Database.MigrateAsync();
  }
}