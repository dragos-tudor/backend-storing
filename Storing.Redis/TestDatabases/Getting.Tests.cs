using Docker.DotNet.Models;
using StackExchange.Redis;

namespace Storing.Redis;

static partial class TestDatabases
{
  internal static string GetRedisEndpoints(NetworkSettings network, int serverPort) =>
    $"{network.IPAddress}:{serverPort}";

  internal static async Task<IDatabase> GetRedisDatabase (int databaseId = 0) =>
    (await RedisClient.Value).GetDatabase(databaseId);
}