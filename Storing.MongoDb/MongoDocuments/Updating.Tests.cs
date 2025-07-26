
namespace Storing.MongoDb;

sealed record Item { public int Id { get; set; } }

sealed record Update1
{
  public string Id { get; set; } = default!;
  public string Text = default!;
  public int Number;
}

sealed record Update2
{
  public string Id { get; set; } = default!;
  public int[] Items = [];
}

sealed record Update3
{
  public string Id { get; set; } = default!;
  public Item[] Items = [];
}


partial class MongoDbTests
{
  [TestMethod]
  public async Task document_field__update__field_updated ()
  {
    var coll = GetMongoCollection<Update1>(Database, "documents");
    var id = Guid.NewGuid().ToString();
    var original = new Update1 { Id = id, Text = "a" };
    var modified = new { text = "b" };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, SetFieldDefinition<Update1, string>(nameof(Update1.Text), modified.text));

    var actual = await coll.AsQueryable().FirstAsync(x => x.Id == id);
    Assert.AreEqual("b", actual.Text);
  }

  [TestMethod]
  public async Task document_fields__update__fields_updated ()
  {
    var coll = GetMongoCollection<Update1>(Database, "documents");
    var id = Guid.NewGuid().ToString();
    var original = new Update1 { Id = id, Text = "a", Number = 1 };
    var modified = new { text = "b", number = 2 };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, CombineDefinitions(
      SetFieldDefinition<Update1, string>(nameof(Update1.Text), modified.text),
      SetFieldDefinition<Update1, int>(nameof(Update1.Number), modified.number)
    ));

    var actual = await coll.AsQueryable().FirstAsync(x => x.Id == id);
    Assert.AreEqual("b", actual.Text);
  }

  [TestMethod]
  public async Task document_values_array__push__array_inserted ()
  {
    var coll = GetMongoCollection<Update2>(Database, "documents");
    var id = Guid.NewGuid().ToString();
    var original = new Update2 { Id = id, Items = [1] };
    var modified = new { items = new int[] { 2, 3 } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, AddToSetEachDefinition<Update2, int>(nameof(Update2.Items), modified.items));

    var actual = await coll.AsQueryable().FirstAsync(x => x.Id == id);
    actual.Items.ShouldBe([1, 2, 3]);
  }

  [TestMethod]
  public async Task document_values_array__pull__array_deleted ()
  {
    var coll = GetMongoCollection<Update2>(Database, "documents");
    var id = Guid.NewGuid().ToString();
    var original = new Update2 { Id = id, Items = [1, 2, 3] };
    var modified = new { items = new int[] { 2, 3 } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, PullAllDefinition<Update2, int>(nameof(Update2.Items), modified.items));

    var actual = await coll.AsQueryable().FirstAsync(x => x.Id == id);
    actual.Items.ShouldBe([1]);
  }

  [TestMethod]
  public async Task document_objects_array__push__field_array_inserted ()
  {
    var coll = GetMongoCollection<Update3>(Database, "documents");
    var id = Guid.NewGuid().ToString();
    var original = new Update3 { Id = id, Items = [new () { Id = 1 }] };
    var modified = new { items = new Item[] { new() { Id = 2 }, new() { Id = 3 } } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, AddToSetEachDefinition<Update3, Item>(nameof(Update3.Items), modified.items));

    var actual = await coll.AsQueryable().FirstAsync(x => x.Id == id);
    actual.Items.Select(x => x.Id).ToArray().ShouldBe([1, 2, 3]);
  }

  [TestMethod]
  public async Task document_objects_array__pull__field_array_deleted ()
  {
    var coll = GetMongoCollection<Update3>(Database, "documents");
    var id = Guid.NewGuid().ToString();
    var original = new Update3 { Id = id, Items = [new() { Id = 1 }, new() { Id = 2 }, new() { Id = 3 }] };
    var modified = new { items = new Item[] { new() { Id = 2 }, new() { Id = 3 } } };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, PullAllDefinition<Update3, Item>(nameof(Update3.Items), modified.items));

    var actual = await coll.AsQueryable().FirstAsync(x => x.Id == id);
    actual.Items.Select(x => x.Id).ShouldBe([1]);
  }

  [TestMethod]
  public async Task document_objects_array__pull_one_item__item_deleted ()
  {
    var coll = GetMongoCollection<Update3>(Database, "documents");
    var id = Guid.NewGuid().ToString();
    var original = new Update3 { Id = id, Items = [new() { Id = 1 }, new() { Id = 2 }, new() { Id = 3 }] };

    await InsertDocument(coll, original);
    await UpdateDocument(coll, original, PullOneFromSetDefinition<Update3, Item>(nameof(Update3.Items), new() { Id = 2 }));

    var actual = await coll.AsQueryable().FirstAsync(x => x.Id == id);
    actual.Items.ShouldBe([new() { Id = 1 }, new() { Id = 3 }]);
  }
}