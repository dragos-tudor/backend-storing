using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlConnectionString (
    string dbName,
    string userName,
    string password,
    string serverName,
    Action<SqlConnectionStringBuilder>? setBuilder = default)
  =>
    new SqlConnectionStringBuilder {
      DataSource = serverName,
      InitialCatalog = dbName,
      UserID = userName,
      Password = password
    }
    .SetSqlConnectionStringBuilder(setBuilder)
    .ToString();
}