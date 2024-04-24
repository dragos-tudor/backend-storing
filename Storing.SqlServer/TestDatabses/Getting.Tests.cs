using Docker.DotNet.Models;

namespace Storing.SqlServer;

static partial class TestDatabases
{
  internal static string GetDbServerIpAddress (NetworkSettings network) =>
    network.IPAddress;
}