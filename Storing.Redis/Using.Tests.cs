global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Storing.Redis.RedisTests;
global using System.Threading;
global using Shouldly;

namespace Storing.Redis;

[TestClass]
public sealed partial class RedisTests
{
  internal static readonly IDatabase Database = GetRedisDatabase(CreateRedisClient(["127.0.0.1:6379"]));
}
