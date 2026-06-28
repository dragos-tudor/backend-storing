
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string? GetSqlServerAdminPassword(string password = "MSSQL_SA_PASSWORD") => Environment.GetEnvironmentVariable(password);
}