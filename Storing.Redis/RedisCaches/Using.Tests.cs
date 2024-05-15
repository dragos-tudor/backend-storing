// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Storing.Redis;

[TestClass]
public partial class RedisCacheTests
{
  [TestMethod]
  public async Task GetMissingCacheKeyReturnsNull()
  {
    var db = await GetRedisDatabase();
    string key = "non-existent-key";

    Assert.IsNull(await GetCacheAsync(db, key));
  }

  [TestMethod]
  public async Task GetFromCacheReturnsObject()
  {
    var db = await GetRedisDatabase();
    var value = new byte[1];
    string key = "myKey1";

    await SetCacheAsync(db, key, value);

    AreEqual(value, await GetCacheAsync(db, key));
  }

  [TestMethod]
  public async Task SetAndGetFromCacheWorksWithCaseSensitiveKeys()
  {
    var db = await GetRedisDatabase();
    var value = new byte[1];
    string key1 = "myKey2";
    string key2 = "MyKey2";

    await SetCacheAsync(db, key1, value);

    AreEqual(value, await GetCacheAsync(db, key1));
    Assert.IsNull(await GetCacheAsync(db, key2));
  }

  [TestMethod]
  public async Task SetToCacheAlwaysOverwrites()
  {
    var db = await GetRedisDatabase();
    var value1 = new byte[1] { 1 };
    string key = "myKey3";

    await SetCacheAsync(db, key, value1);

    AreEqual(value1, await GetCacheAsync(db, key));

    var value2 = new byte[1] { 2 };
    await SetCacheAsync(db, key, value2);

    AreEqual(value2, await GetCacheAsync(db, key));
  }

  [TestMethod]
  public async Task GetMissingStringCacheKeyReturnsNull()
  {
    var db = await GetRedisDatabase();
    string key = "non-existent-key";

    Assert.IsNull(await GetStringCacheAsync(db, key));
  }

  [TestMethod]
  public async Task GetStringFromCacheReturnsString()
  {
    var db = await GetRedisDatabase();
    var value = "some text";
    string key = "myKey4";

    await SetStringCacheAsync(db, key, value);

    Assert.AreEqual(value, await GetStringCacheAsync(db, key));
  }

  [TestMethod]
  public async Task SetAndGetStringFromCacheWorksWithCaseSensitiveKeys()
  {
    var db = await GetRedisDatabase();
    var value = "some text";
    string key1 = "myKey5";
    string key2 = "MyKey5";

    await SetStringCacheAsync(db, key1, value);

    Assert.AreEqual(value, await GetStringCacheAsync(db, key1));
    Assert.IsNull(await GetStringCacheAsync(db, key2));
  }

  [TestMethod]
  public async Task SetStringToCacheAlwaysOverwrites()
  {
    var db = await GetRedisDatabase();
    var value1 = "some text 1";
    string key = "myKey6";

    await SetStringCacheAsync(db, key, value1);

    Assert.AreEqual(value1, await GetStringCacheAsync(db, key));

    var value2 = "some text 2";
    await SetStringCacheAsync(db, key, value2);

    Assert.AreEqual(value2, await GetStringCacheAsync(db, key));
  }

  [TestMethod]
  public async Task RemoveCacheKey()
  {
    var db = await GetRedisDatabase();
    var value = new byte[1];
    string key = "myKey7";

    await SetCacheAsync(db, key, value);

    AreEqual(value, await GetCacheAsync(db, key));

    await RemoveCacheAsync(db, key);

    Assert.IsNull(await GetCacheAsync(db, key));
  }
}