
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  static string GetCreateSqlLoginScript(string userName, string password) => $@"
    USE MASTER;
    IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = '{userName}')
    BEGIN
      CREATE LOGIN {userName} WITH PASSWORD = '{password}'
    END
  ";

  static string GetCreateSqlDatabaseUserScript(string databaseName, string userName) => $@"
    USE [{databaseName}];
    IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'{userName}')
    BEGIN
        CREATE USER [{userName}] FOR LOGIN [{userName}]
        EXEC sp_addrolemember N'db_datawriter', N'{userName}'
        EXEC sp_addrolemember N'db_datareader', N'{userName}'
    END;
  ";

}