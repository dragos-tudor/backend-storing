global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Storing.Redis.RedisTests;
global using System.Threading;
global using Shouldly;

namespace Storing.Redis;

[TestClass]
public sealed partial class RedisTests
{
    static readonly IConnectionMultiplexer Client = CreateRedisClient(["redis"], GetRedisAdminUserName(), GetRedisAdminPassword());
    internal static readonly IDatabase Database = GetRedisDatabase(Client);
    readonly TestContext TestContext;

    public RedisTests(TestContext testContext) => TestContext = testContext;
}
