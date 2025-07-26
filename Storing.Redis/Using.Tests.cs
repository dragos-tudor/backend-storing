global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Storing.Redis.RedisTests;
global using System.Threading;
global using Shouldly;

using StackExchange.Redis;

namespace Storing.Redis;

[TestClass]
public sealed partial class RedisTests
{
  internal static readonly IDatabase Database = GetRedisDatabase(CreateRedisClient(GetRedisEndpoints("127.0.0.1", 6379), "test"));
}
