
namespace Storing.SqlServer;

partial class SqlServerTests
{
  const string ContainerName = "storing-sql";
  const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";

  const string AdminName = "sa";
  const string AdminPassword = "admin.P@ssw0rd";

  const int ServerPort = 1433;
  static string ServerIpAddress = string.Empty;

  static void StartSqlServer()
  {
    var networkSettings = StartSqlContainer(ServerPort, AdminPassword, ImageName, ContainerName);
    ServerIpAddress = GetServerIpAddress(networkSettings);
  }
}