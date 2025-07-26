// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Storing.Redis;

[TestClass]
public class RedisCacheTests1
{
  [TestMethod]
  public async Task AbsoluteExpirationExpires()
  {
    var key = "expKey1";
    var value = new byte[1];

    var futureExpiration = TimeSpan.FromSeconds(1);
    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(futureExpiration));

    (await GetCacheAsync(Database, key)).ShouldBe(value);

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task AbsoluteSubSecondExpirationExpiresImmediately()
  {
    var key = "expKey2";
    var value = new byte[1];

    var immediatelyExpiration = TimeSpan.FromSeconds(0.25);
    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(immediatelyExpiration));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }
}

[TestClass]
public class RedisCacheTests2
{
  [TestMethod]
  public async Task RelativeExpirationExpires()
  {
    var key = "expKey3";
    var value = new byte[1];

    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(1)));

    (await GetCacheAsync(Database, key)).ShouldBe(value);

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task RelativeSubSecondExpirationExpiresImmediately()
  {
    var key = "expKey4";
    var value = new byte[1];

    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(0.25)));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }
}

[TestClass]
public class RedisCacheTests3
{
  [TestMethod]
  public async Task SlidingExpirationExpiresIfNotAccessed()
  {
    var key = "expKey5";
    var value = new byte[1];

    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(1)));

    (await GetCacheAsync(Database, key)).ShouldBe(value);

    await Task.Delay(TimeSpan.FromSeconds(1.5));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task SlidingSubSecondExpirationExpiresImmediately()
  {
    var key = "expKey6";
    var value = new byte[1];

    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(0.25)));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }
}

[TestClass]
public class RedisCacheTests4
{
  [TestMethod]
  public async Task SlidingExpirationReneweDatabaseyAccess()
  {
    if(Environment.GetEnvironmentVariable("IN_MEMORY")! == "true")
      return;

    var key = "expKey7";
    var value = new byte[1];

    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(1)));

    for (int i = 0; i < 4; i++)
    {
      (await GetCacheAsync(Database, key)).ShouldBe(value);

      await Task.Delay(TimeSpan.FromSeconds(0.5));
    }

    await Task.Delay(TimeSpan.FromSeconds(1));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }
}

[TestClass]
public class RedisCacheTests5
{
  [TestMethod]
  public async Task SlidingExpirationReneweDatabaseyAccessUntilAbsoluteExpiration()
  {
    if(Environment.GetEnvironmentVariable("IN_MEMORY")! == "true")
      return;

    var key = "expKey8";
    var value = new byte[1];

    await SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions()
      .SetSlidingExpiration(TimeSpan.FromSeconds(1))
      .SetAbsoluteExpiration(TimeSpan.FromSeconds(2)));

    for (int i = 0; i < 4; i++)
    {
      (await GetCacheAsync(Database, key)).ShouldBe(value);

      await Task.Delay(TimeSpan.FromSeconds(0.5));
    }

    await Task.Delay(TimeSpan.FromSeconds(1));

    Assert.IsNull(await GetCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task AbsoluteExpirationInThePastThrows()
  {
    var key = "expKey9";
    var value = new byte[1];

    var pastExpiration = DateTimeOffset.Now - TimeSpan.FromMinutes(1);
    await Assert.ThrowsExceptionAsync<AbsoluteExpirationException>(() =>
      SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(pastExpiration)));
  }

  [TestMethod]
  public async Task NegativeRelativeExpirationThrows()
  {
    var key = "expKey10";
    var value = new byte[1];

    var negativeExpiration = TimeSpan.FromMinutes(-1);
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(negativeExpiration)));
  }

  [TestMethod]
  public async Task ZeroRelativeExpirationThrows()
  {
    var key = "expKey11";
    var value = new byte[1];

    var zeroExpiration = TimeSpan.Zero;
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(zeroExpiration)));
  }

  [TestMethod]
  public async Task NegativeSlidingExpirationThrows()
  {
    var key = "expKey12";
    var value = new byte[1];

    var negativeExpration = TimeSpan.FromMinutes(-1);
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(negativeExpration)));
  }

  [TestMethod]
  public async Task ZeroSlidingExpirationThrows()
  {
    var key = "expKey13";
    var value = new byte[1];

    var zeroExpiration = TimeSpan.Zero;
    await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
      SetCacheAsync(Database, key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(zeroExpiration)));
  }
}