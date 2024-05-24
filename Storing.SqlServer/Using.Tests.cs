global using System.Threading;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;

namespace Storing.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    const string adminName = "sa";
    const string adminPassword = "admin.P@ssw0rd";
    const string containerName = "storing-sql";
    const string imageName = "mcr.microsoft.com/mssql/server:2019-latest";
    const int serverPort = 1433;

    var serverAddress = StartSqlServer(adminPassword, imageName, containerName, serverPort);

    EntitiesConnString = CreateSqlConnectionString("entities", adminName, adminPassword, serverAddress);
    QueriesConnString = CreateSqlConnectionString("queries", adminName, adminPassword, serverAddress);
    TrackingConnString = CreateSqlConnectionString("tracking", adminName, adminPassword, serverAddress);

    CleanEntitiesDatabase(EntitiesConnString);
    CleanQueriesDatabase(QueriesConnString);
  }
}