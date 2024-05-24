
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static void CleanEntitiesDatabase (string connString)
  {
    using var entitiesContext = CreateEntitiesContext(connString);
    entitiesContext.Database.EnsureCreated();
    entitiesContext.Database.ExecuteSql($@"
      DELETE FROM AuthorBook;
      DELETE FROM Authors;
      DELETE FROM Books"
    );
  }

  static void CleanQueriesDatabase (string connString)
  {
    using var queriesContext = CreateQueriesContext(connString);
    queriesContext.Database.EnsureCreated();
    queriesContext.Database.ExecuteSql($@"
      DELETE FROM Pages;
      DELETE FROM Orders;
      DELETE FROM Filters;
    ");
  }
}