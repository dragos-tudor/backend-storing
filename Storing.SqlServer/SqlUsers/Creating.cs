
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static void CreateSqlDatabaseUser<TContext>(TContext context, string dbName, string userName, string password) where TContext : DbContext
  {
    context.Database.ExecuteSqlRaw(GetCreateSqlLoginScript(userName, password));
    context.Database.ExecuteSqlRaw(GetCreateSqlDatabaseUserScript(dbName, userName));
  }
}