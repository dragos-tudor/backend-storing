
namespace Storing.SqlServer;

partial class SqlServerTests
{
  const int ServerPort = 1433;

  static string ServerIpAddress = string.Empty;

  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    var networkSettings = StartSqlContainer(ServerPort, AdminPassword, ImageName, ContainerName);
    ServerIpAddress = GetServerIpAddress(networkSettings);

    using var entitiesContext = CreateEntitiesContext();
    entitiesContext.Database.EnsureCreated();
    entitiesContext.Database.ExecuteSql($@"
      DELETE FROM AuthorBook;
      DELETE FROM Authors;
      DELETE FROM Books"
    );

    using var queriesContext = CreateQueriesContext();
    queriesContext.Database.ExecuteSql($@"
      DELETE FROM Pages;
      DELETE FROM Orders;
      DELETE FROM Filters;
    ");
  }
}