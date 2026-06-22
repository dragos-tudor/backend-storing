
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string GetSqlConnectionStringServerPort (int serverPort) => serverPort > 0 ? $",{serverPort}" : string.Empty;
}