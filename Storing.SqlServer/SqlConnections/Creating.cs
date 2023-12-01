using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

public static partial class SqlConnections
{
  public static string CreateSqlConnection(
    string dbName,
    string userName,
    string password,
    string serverName,
    Action<SqlConnectionStringBuilder>? setBuilder = default) =>
      new SqlConnectionStringBuilder {
        DataSource = serverName,
        InitialCatalog = dbName,
        UserID = userName,
        Password = password
      }
      .TrySetSqlConnectionStringBuilder(setBuilder)
      .ToString();

}