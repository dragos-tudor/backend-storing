// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using static Storing.Redis.RedisExpirations;
using static Storing.Redis.RedisCache;

namespace Storing.Redis;

[TestClass]
public class RedisCacheTests1
{
  [TestMethod]
  public async Task AbsoluteExpirationExpires()
  {
    var db = await GetRedisDatabase();
    var key = "expKey1";
    var value = new byte[1];

    var futureExpiration = TimeSpan.FromSeconds(1);
    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(futureExpiration));

    AssertExtensions.AreEqual(value, await GetCacheAsync(db, key));

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.IsNull(await GetCacheAsync(db, key));
  }

  [TestMethod]
  public async Task AbsoluteSubSecondExpirationExpiresImmediately()
  {
    var db = await GetRedisDatabase();
    var key = "expKey2";
    var value = new byte[1];

    var immediatelyExpiration = TimeSpan.FromSeconds(0.25);
    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(immediatelyExpiration));

    Assert.IsNull(await GetCacheAsync(db, key));
  }
}

[TestClass]
public class RedisCacheTests2
{
  [TestMethod]
  public async Task RelativeExpirationExpires()
  {
    var db = await GetRedisDatabase();
    var key = "expKey3";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(1)));

    AssertExtensions.AreEqual(value, await GetCacheAsync(db, key));

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.IsNull(await GetCacheAsync(db, key));
  }

  [TestMethod]
  public async Task RelativeSubSecondExpirationExpiresImmediately()
  {
    var db = await GetRedisDatabase();
    var key = "expKey4";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(0.25)));

    Assert.IsNull(await GetCacheAsync(db, key));
  }
}

[TestClass]
public class RedisCacheTests3
{
  [TestMethod]
  public async Task SlidingExpirationExpiresIfNotAccessed()
  {
    var db = await GetRedisDatabase();
    var key = "expKey5";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(1)));

    AssertExtensions.AreEqual(value, await GetCacheAsync(db, key));

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.IsNull(await GetCacheAsync(db, key));
  }

  [TestMethod]
  public async Task SlidingSubSecondExpirationExpiresImmediately()
  {
    var db = await GetRedisDatabase();
    var key = "expKey6";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(0.25)));

    Assert.IsNull(await GetCacheAsync(db, key));
  }
}

[TestClass]
public class RedisCacheTests4
{
  [TestMethod]
  public async Task SlidingExpirationRenewedByAccess()
  {
    if(Environment.GetEnvironmentVariable("IN_MEMORY")! == "true")
      return;

    var db = await GetRedisDatabase();
    var key = "expKey7";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(1)));

    for (int i = 0; i < 4; i++)
    {
      AssertExtensions.AreEqual(value, await GetCacheAsync(db, key));

      await Task.Delay(TimeSpan.FromSeconds(0.5));
    }

    await Task.Delay(TimeSpan.FromSeconds(1));

    Assert.IsNull(await GetCacheAsync(db, key));
  }
}

[TestClass]
public class RedisCacheTests5
{
  [TestMethod]
  public async Task SlidingExpirationRenewedByAccessUntilAbsoluteExpiration()
  {
    if(Environment.GetEnvironmentVariable("IN_MEMORY")! == "true")
      return;

    var db = await GetRedisDatabase();
    var key = "expKey8";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions()
      .SetSlidingExpiration(TimeSpan.FromSeconds(1))
      .SetAbsoluteExpiration(TimeSpan.FromSeconds(2)));

    for (int i = 0; i < 4; i++)
    {
      AssertExtensions.AreEqual(value, await GetCacheAsync(db, key));

      await Task.Delay(TimeSpan.FromSeconds(0.5));
    }

    await Task.Delay(TimeSpan.FromSeconds(1));

    Assert.IsNull(await GetCacheAsync(db, key));
  }

  [TestMethod]
  public async Task AbsoluteExpirationInThePastThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey9";
    var value = new byte[1];

    var pastExpiration = DateTimeOffset.Now - TimeSpan.FromMinutes(1);
    await Assert.ThrowsExceptionAsync<AbsoluteExpirationException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(pastExpiration)));
  }

  [TestMethod]
  public async Task NegativeRelativeExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey10";
    var value = new byte[1];

    var negativeExpiration = TimeSpan.FromMinutes(-1);
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(negativeExpiration)));
  }

  [TestMethod]
  public async Task ZeroRelativeExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey11";
    var value = new byte[1];

    var zeroExpiration = TimeSpan.Zero;
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(zeroExpiration)));
  }

  [TestMethod]
  public async Task NegativeSlidingExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey12";
    var value = new byte[1];

    var negativeExpration = TimeSpan.FromMinutes(-1);
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(negativeExpration)));
  }

  [TestMethod]
  public async Task ZeroSlidingExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey13";
    var value = new byte[1];

    var zeroExpiration = TimeSpan.Zero;
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(zeroExpiration)));
  }
}