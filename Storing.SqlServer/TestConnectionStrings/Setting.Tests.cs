
using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerTests
{
  static void SetConectionStringBuilder (SqlConnectionStringBuilder builder) {
    builder.ConnectTimeout = 3;
    builder.TrustServerCertificate = true;
  }
}