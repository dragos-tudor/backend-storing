using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlConnectionString(
    string dbName, string userName,
    string password, string serverName, bool trustServerCertificate = true,
    Action<SqlConnectionStringBuilder>? setBuilder = default) =>
    SetSqlConnectionStringBuilder(
      new SqlConnectionStringBuilder
      {
        DataSource = serverName,
        InitialCatalog = dbName,
        UserID = userName,
        Password = password,
        TrustServerCertificate = trustServerCertificate
      },
      setBuilder
    )
    .ToString();
}