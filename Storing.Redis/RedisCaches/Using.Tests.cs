// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Storing.Redis;

[TestClass]
public partial class RedisCacheTests
{
  [TestMethod]
  public async Task GetMissingCacheKeyReturnsNull()
  {
    string key = "non-existent-key";

    Assert.IsNull(await GetCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task GetFromCacheReturnsObject()
  {
    var value = new byte[1];
    string key = "myKey1";

    await SetCacheAsync(Database, key, value);

    AreEqual(value, await GetCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task SetAndGetFromCacheWorksWithCaseSensitiveKeys()
  {
    var value = new byte[1];
    string key1 = "myKey2";
    string key2 = "MyKey2";

    await SetCacheAsync(Database, key1, value);

    AreEqual(value, await GetCacheAsync(Database, key1));
    Assert.IsNull(await GetCacheAsync(Database, key2));
  }

  [TestMethod]
  public async Task SetToCacheAlwaysOverwrites()
  {
    var value1 = new byte[1] { 1 };
    string key = "myKey3";

    await SetCacheAsync(Database, key, value1);

    AreEqual(value1, await GetCacheAsync(Database, key));

    var value2 = new byte[1] { 2 };
    await SetCacheAsync(Database, key, value2);

    AreEqual(value2, await GetCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task GetMissingStringCacheKeyReturnsNull()
  {
    string key = "non-existent-key";

    Assert.IsNull(await GetStringCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task GetStringFromCacheReturnsString()
  {
    var value = "some text";
    string key = "myKey4";

    await SetStringCacheAsync(Database, key, value);

    Assert.AreEqual(value, await GetStringCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task SetAndGetStringFromCacheWorksWithCaseSensitiveKeys()
  {
    var value = "some text";
    string key1 = "myKey5";
    string key2 = "MyKey5";

    await SetStringCacheAsync(Database, key1, value);

    Assert.AreEqual(value, await GetStringCacheAsync(Database, key1));
    Assert.IsNull(await GetStringCacheAsync(Database, key2));
  }

  [TestMethod]
  public async Task SetStringToCacheAlwaysOverwrites()
  {
    var value1 = "some text 1";
    string key = "myKey6";

    await SetStringCacheAsync(Database, key, value1);

    Assert.AreEqual(value1, await GetStringCacheAsync(Database, key));

    var value2 = "some text 2";
    await SetStringCacheAsync(Database, key, value2);

    Assert.AreEqual(value2, await GetStringCacheAsync(Database, key));
  }

  [TestMethod]
  public async Task RemoveCacheKey()
  {
    var value = new byte[1];
    string key = "myKey7";

    await SetCacheAsync(Database, key, value);

    AreEqual(value, await GetCacheAsync(Database, key));

    await RemoveCacheAsync(Database, key);

    Assert.IsNull(await GetCacheAsync(Database, key));
  }
}