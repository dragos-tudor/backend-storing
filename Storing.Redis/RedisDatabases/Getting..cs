
namespace Storing.Redis;

partial class RedisFuncs
{
    public static IDatabase GetRedisDatabase(IConnectionMultiplexer client, int databaseId = 0) => client.GetDatabase(databaseId);
}