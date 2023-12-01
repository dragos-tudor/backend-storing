// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using static Storing.Redis.RedisExpirations;
using static Storing.Redis.RedisCache;

namespace Storing.Redis;

// run tests in parallel [use different test classes]
public class RedisCacheTests1 {

  [Fact]
  internal async Task AbsoluteExpirationExpires()
  {
    var db = await GetRedisDatabase();
    var key = "expKey1";
    var value = new byte[1];

    var futureExpiration = TimeSpan.FromSeconds(1);
    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(futureExpiration));

    Assert.Equal(value, await GetCacheAsync(db, key));

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.Null(await GetCacheAsync(db, key));
  }

  [Fact]
  internal async Task AbsoluteSubSecondExpirationExpiresImmediately()
  {
    var db = await GetRedisDatabase();
    var key = "expKey2";
    var value = new byte[1];

    var immediatelyExpiration = TimeSpan.FromSeconds(0.25);
    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(immediatelyExpiration));

    Assert.Null(await GetCacheAsync(db, key));
  }
}

public class RedisCacheTests2 {

  [Fact]
  internal async Task RelativeExpirationExpires()
  {
    var db = await GetRedisDatabase();
    var key = "expKey3";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(1)));

    Assert.Equal(value, await GetCacheAsync(db, key));

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.Null(await GetCacheAsync(db, key));
  }

  [Fact]
  internal async Task RelativeSubSecondExpirationExpiresImmediately()
  {
    var db = await GetRedisDatabase();
    var key = "expKey4";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(0.25)));

    Assert.Null(await GetCacheAsync(db, key));
  }

}

public class RedisCacheTests3 {

  [Fact]
  internal async Task SlidingExpirationExpiresIfNotAccessed()
  {
    var db = await GetRedisDatabase();
    var key = "expKey5";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(1)));

    Assert.Equal(value, await GetCacheAsync(db, key));

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.Null(await GetCacheAsync(db, key));
  }

  [Fact]
  internal async Task SlidingSubSecondExpirationExpiresImmediately()
  {
    var db = await GetRedisDatabase();
    var key = "expKey6";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(0.25)));

    Assert.Null(await GetCacheAsync(db, key));
  }

}

public class RedisCacheTests4 {

  [Fact]
  internal async Task SlidingExpirationRenewedByAccess()
  {
    if(Environment.GetEnvironmentVariable("IN_MEMORY")! == "true")
      return;

    var db = await GetRedisDatabase();
    var key = "expKey7";
    var value = new byte[1];

    await SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(1)));

    for (int i = 0; i < 4; i++)
    {
      Assert.Equal(value, await GetCacheAsync(db, key));

      await Task.Delay(TimeSpan.FromSeconds(0.5));
    }

    await Task.Delay(TimeSpan.FromSeconds(1));

    Assert.Null(await GetCacheAsync(db, key));
  }

}

public class RedisCacheTests5 {

  [Fact]
  internal async Task SlidingExpirationRenewedByAccessUntilAbsoluteExpiration()
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
      Assert.Equal(value, await GetCacheAsync(db, key));

      await Task.Delay(TimeSpan.FromSeconds(0.5));
    }

    await Task.Delay(TimeSpan.FromSeconds(1));

    Assert.Null(await GetCacheAsync(db, key));
  }

  [Fact]
  internal async Task AbsoluteExpirationInThePastThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey9";
    var value = new byte[1];

    var pastExpiration = DateTimeOffset.Now - TimeSpan.FromMinutes(1);
    await Assert.ThrowsAsync<AbsoluteExpirationException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(pastExpiration)));
  }

  [Fact]
  internal async Task NegativeRelativeExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey10";
    var value = new byte[1];

    var negativeExpiration = TimeSpan.FromMinutes(-1);
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(negativeExpiration)));
  }

  [Fact]
  internal async Task ZeroRelativeExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey11";
    var value = new byte[1];

    var zeroExpiration = TimeSpan.Zero;
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(zeroExpiration)));
  }

  [Fact]
  internal async Task NegativeSlidingExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey12";
    var value = new byte[1];

    var negativeExpration = TimeSpan.FromMinutes(-1);
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(negativeExpration)));
  }

  [Fact]
  internal async Task ZeroSlidingExpirationThrows()
  {
    var db = await GetRedisDatabase();
    var key = "expKey13";
    var value = new byte[1];

    var zeroExpiration = TimeSpan.Zero;
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(db, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(zeroExpiration)));
  }

}