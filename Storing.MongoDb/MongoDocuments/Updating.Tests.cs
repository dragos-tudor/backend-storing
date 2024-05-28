
namespace Storing.MongoDb;

sealed record Item { public int Id { get; set; } }

sealed record Update1
{
  public string Id { get; set; } = default!;
  public string u1str = default!;
  public int u1int;
}

sealed record Update2
{
  public string Id { get; set; } = default!;
  public int[] u2coll = [];
}

sealed record Update3
{
  public string Id { get; set; } = default!;
  public Item[] u3coll = [];
}


partial class MongoDbTests
{
  [TestMethod]
  public async Task document_field__update__field_updated ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Update1>(db);
    var original = new Update1 { Id = id, u1str = "a" };
    var modified = new { text1 = "b" };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, SetFieldDefinition<Update1, string>(nameof(Update1.u1str), modified.text1));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x.Id == id);
    Assert.AreEqual("b", actual.u1str);
  }

  [TestMethod]
  public async Task document_fields__update__fields_updated ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Update1>(db);
    var original = new Update1 { Id = id, u1str = "a", u1int = 1 };
    var modified = new { text1 = "b", int1 = 2 };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, CombineDefinitions(
      SetFieldDefinition<Update1, string>(nameof(Update1.u1str), modified.text1),
      SetFieldDefinition<Update1, int>(nameof(Update1.u1int), modified.int1)
    ));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x.Id == id);
    Assert.AreEqual("b", actual.u1str);
  }

  [TestMethod]
  public async Task document_values_array__push__array_inserted ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Update2>(db);
    var original = new Update2 { Id = id, u2coll = [1] };
    var modified = new { coll2 = new int[] { 2, 3 } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, AddToSetEachDefinition<Update2, int>(nameof(Update2.u2coll), modified.coll2));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x.Id == id);
    AreEqual([1, 2, 3], actual.u2coll);
  }

  [TestMethod]
  public async Task document_values_array__pull__array_deleted ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Update2>(db);
    var original = new Update2 { Id = id, u2coll = [1, 2, 3] };
    var modified = new { coll2 = new int[] { 2, 3 } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, PullAllDefinition<Update2, int>(nameof(Update2.u2coll), modified.coll2));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x.Id == id);
    AreEqual([1], actual.u2coll);
  }

  [TestMethod]
  public async Task document_objects_array__push__field_array_inserted ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Update3>(db);
    var original = new Update3 { Id = id, u3coll = [new () { Id = 1 }] };
    var modified = new { coll3 = new Item[] { new() { Id = 2 }, new() { Id = 3 } } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, AddToSetEachDefinition<Update3, Item>(nameof(Update3.u3coll), modified.coll3));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x.Id == id);
    AreEqual([1, 2, 3], actual.u3coll.Select(x => x.Id).ToArray());
  }

  [TestMethod]
  public async Task document_objects_array__pull__field_array_deleted ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Update3>(db);
    var original = new Update3 { Id = id, u3coll = [new() { Id = 1 }, new() { Id = 2 }, new() { Id = 3 }] };
    var modified = new { coll3 = new Item[] { new() { Id = 2 }, new() { Id = 3 } } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, PullAllDefinition<Update3, Item>(nameof(Update3.u3coll), modified.coll3));

    var actual = await coll
      .AsQueryable()
      .FirstAsync(x => x.Id == id);
    AreEqual([1], actual.u3coll.Select(x => x.Id));
  }

}