
using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerTests
{
  static void SetSqlConectionStringBuilder (SqlConnectionStringBuilder builder) {
    builder.ConnectTimeout = 3;
    builder.TrustServerCertificate = true;
  }
}