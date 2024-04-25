using static Storing.SqlServer.TestContainers;
using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

static partial class TestServers
{
  const int ServerPort = 1433;

  internal static readonly Lazy<Task<string>> ServerIpAddress = new (async () =>
    GetDbServerIpAddress(await StartSqlContainerAsync(ServerPort, AdminPassword)));
}