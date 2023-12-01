// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using static Storing.Redis.RedisCache;

namespace Storing.Redis;

public partial class RedisCacheTests {

  [Fact]
  internal async Task GetMissingCacheKeyReturnsNull()
  {
    var db = await GetRedisDatabase();
    string key = "non-existent-key";

    Assert.Null(await GetCacheAsync(db, key));
  }

  [Fact]
  internal async Task GetFromCacheReturnsObject()
  {
    var db = await GetRedisDatabase();
    var value = new byte[1];
    string key = "myKey1";

    await SetCacheAsync(db, key, value);

    Assert.Equal(value, await GetCacheAsync(db, key));
  }

  [Fact]
  internal async Task SetAndGetFromCacheWorksWithCaseSensitiveKeys()
  {
    var db = await GetRedisDatabase();
    var value = new byte[1];
    string key1 = "myKey2";
    string key2 = "MyKey2";

    await SetCacheAsync(db, key1, value);

    Assert.Equal(value, await GetCacheAsync(db, key1));
    Assert.Null(await GetCacheAsync(db, key2));
  }

  [Fact]
  internal async Task SetToCacheAlwaysOverwrites()
  {
    var db = await GetRedisDatabase();
    var value1 = new byte[1] { 1 };
    string key = "myKey3";

    await SetCacheAsync(db, key, value1);

    Assert.Equal(value1, await GetCacheAsync(db, key));

    var value2 = new byte[1] { 2 };
    await SetCacheAsync(db, key, value2);

    Assert.Equal(value2, await GetCacheAsync(db, key));
  }

  [Fact]
  internal async Task GetMissingStringCacheKeyReturnsNull()
  {
    var db = await GetRedisDatabase();
    string key = "non-existent-key";

    Assert.Null(await GetStringCacheAsync(db, key));
  }

  [Fact]
  internal async Task GetStringFromCacheReturnsString()
  {
    var db = await GetRedisDatabase();
    var value = "some text";
    string key = "myKey4";

    await SetStringCacheAsync(db, key, value);

    Assert.Equal(value, await GetStringCacheAsync(db, key));
  }

  [Fact]
  internal async Task SetAndGetStringFromCacheWorksWithCaseSensitiveKeys()
  {
    var db = await GetRedisDatabase();
    var value = "some text";
    string key1 = "myKey5";
    string key2 = "MyKey5";

    await SetStringCacheAsync(db, key1, value);

    Assert.Equal(value, await GetStringCacheAsync(db, key1));
    Assert.Null(await GetStringCacheAsync(db, key2));
  }

  [Fact]
  internal async Task SetStringToCacheAlwaysOverwrites()
  {
    var db = await GetRedisDatabase();
    var value1 = "some text 1";
    string key = "myKey6";

    await SetStringCacheAsync(db, key, value1);

    Assert.Equal(value1, await GetStringCacheAsync(db, key));

    var value2 = "some text 2";
    await SetStringCacheAsync(db, key, value2);

    Assert.Equal(value2, await GetStringCacheAsync(db, key));
  }

  [Fact]
  internal async Task RemoveCacheKey()
  {
    var db = await GetRedisDatabase();
    var value = new byte[1];
    string key = "myKey7";

    await SetCacheAsync(db, key, value);

    Assert.Equal(value, await GetCacheAsync(db, key));

    await RemoveCacheAsync(db, key);

    Assert.Null(await GetCacheAsync(db, key));
  }

}