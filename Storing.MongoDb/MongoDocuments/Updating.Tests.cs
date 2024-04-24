using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

sealed record Item: Id<int> {}

sealed record Update1: Id<string>
{
  public string u1str = string.Empty;
  public int u1int;
}

sealed record Update2: Id<string>
{
  public int[] u2coll = [];
}

sealed record Update3: Id<string>
{
  public Item[] u3coll = [];
}


public partial class MongoDocumentsTests
{
  [TestMethod]
  public async Task document_field__update__field_updated ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Update1>(db, DbCollection);
    var original = new Update1 { _Id = id, u1str = "a" };
    var modified = new { text1 = "b" };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, SetFieldDefinition<Update1, string>(nameof(Update1.u1str), modified.text1));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x._Id == id);
    Assert.AreEqual("b", actual.u1str);
  }

  [TestMethod]
  public async Task document_fields__update__fields_updated ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Update1>(db, DbCollection);
    var original = new Update1 { _Id = id, u1str = "a", u1int = 1 };
    var modified = new { text1 = "b", int1 = 2 };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, CombineDefinitions(
      SetFieldDefinition<Update1, string>(nameof(Update1.u1str), modified.text1),
      SetFieldDefinition<Update1, int>(nameof(Update1.u1int), modified.int1)
    ));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x._Id == id);
    Assert.AreEqual("b", actual.u1str);
  }

  [TestMethod]
  public async Task document_values_array__push__array_inserted ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Update2>(db, DbCollection);
    var original = new Update2 { _Id = id, u2coll = [1] };
    var modified = new { coll2 = new int[] { 2, 3 } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, AddToSetEachDefinition<Update2, int>(nameof(Update2.u2coll), modified.coll2));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x._Id == id);
    AssertExtensions.AreEqual([1, 2, 3], actual.u2coll);
  }

  [TestMethod]
  public async Task document_values_array__pull__array_deleted ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Update2>(db, DbCollection);
    var original = new Update2 { _Id = id, u2coll = [1, 2, 3] };
    var modified = new { coll2 = new int[] { 2, 3 } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, PullAllDefinition<Update2, int>(nameof(Update2.u2coll), modified.coll2));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x._Id == id);
    AssertExtensions.AreEqual([1], actual.u2coll);
  }

  [TestMethod]
  public async Task document_objects_array__push__field_array_inserted ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Update3>(db, DbCollection);
    var original = new Update3 { _Id = id, u3coll = [new () { _Id = 1 }] };
    var modified = new { coll3 = new Item[] { new() { _Id = 2 }, new() { _Id = 3 } } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, AddToSetEachDefinition<Update3, Item>(nameof(Update3.u3coll), modified.coll3));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x._Id == id);
    AssertExtensions.AreEqual([1, 2, 3], actual.u3coll.Select(x => x._Id).ToArray());
  }

  [TestMethod]
  public async Task document_objects_array__pull__field_array_deleted ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Update3>(db, DbCollection);
    var original = new Update3 { _Id = id, u3coll = [new() { _Id = 1 }, new() { _Id = 2 }, new() { _Id = 3 }] };
    var modified = new { coll3 = new Item[] { new() { _Id = 2 }, new() { _Id = 3 } } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, PullAllDefinition<Update3, Item>(nameof(Update3.u3coll), modified.coll3));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x._Id == id);
    AssertExtensions.AreEqual([1], actual.u3coll.Select(x => x._Id));
  }

}