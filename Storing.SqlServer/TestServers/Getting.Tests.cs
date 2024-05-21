using Docker.DotNet.Models;

namespace Storing.SqlServer;

partial class SqlServerTests
{
  static string GetServerIpAddress (NetworkSettings network) => network.IPAddress;
}