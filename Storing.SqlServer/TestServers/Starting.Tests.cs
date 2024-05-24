
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static string StartSqlServer(string adminPassword, string imageName, string containerName, int serverPort)
  {
    var networkSettings = StartSqlContainer(serverPort, adminPassword, imageName, containerName);
    return GetServerIpAddress(networkSettings);
  }
}