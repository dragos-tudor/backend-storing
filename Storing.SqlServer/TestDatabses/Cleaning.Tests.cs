
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static void CleanEntitiesDatabase ()
  {
    using var entitiesContext = CreateEntitiesContext();
    entitiesContext.Database.EnsureCreated();
    entitiesContext.Database.ExecuteSql($@"
      DELETE FROM AuthorBook;
      DELETE FROM Authors;
      DELETE FROM Books"
    );
  }

  static void CleanQueriesDatabase ()
  {
    using var queriesContext = CreateQueriesContext();
    queriesContext.Database.EnsureCreated();
    queriesContext.Database.ExecuteSql($@"
      DELETE FROM Pages;
      DELETE FROM Orders;
      DELETE FROM Filters;
    ");
  }
}