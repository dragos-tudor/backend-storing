
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string GetSqlConnectionStringPort (int port) => port > 0 ? $",{port}" : string.Empty;
}