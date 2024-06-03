
namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Page), Required = true)]
sealed record Page
{
  public string Id { get; set; } = default!;
  public string text = string.Empty;
}

partial class MongoDbTests
{
  [TestMethod]
  public async Task documents__page__paged_documents ()
  {
    var coll = GetMongoCollection<Page>(Database, "queries");
    var query = coll.AsDiscriminable();
    await InsertDocuments(coll, [
      new Page { Id = Guid.NewGuid().ToString(), text = "0" },
      new Page { Id = Guid.NewGuid().ToString(), text = "1" },
      new Page { Id = Guid.NewGuid().ToString(), text = "2" },
      new Page { Id = Guid.NewGuid().ToString(), text = "3" }
    ]);

    AreEqual([ "0", "1" ], await query.Page(2, 0).Select(x => x.text).ToListAsync());
    AreEqual([ "2", "3" ], await query.Page(2, 1).Select(x => x.text).ToListAsync());
    AreEqual([ "3" ], await query.Page(3, 1).Select(x => x.text).ToListAsync());
    AreEqual([], await query.Page(2, 2).Select(x => x.text).ToListAsync());
    AreEqual([ "0", "1" ], await query.Page(2, null).Select(x => x.text).ToListAsync());
  }
}