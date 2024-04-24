using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoDocuments;
using static Storing.MongoDb.MongoCollections;

namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Page), Required = true)]
sealed record Page : Id<string>
{
  public string text = string.Empty;
}

public partial class MongoQueriesTests
{
  [TestMethod]
  public async Task documents__page__paged_documents ()
  {
    var db = await GetMongoDatabase();
    var coll = GetCollection<Page>(db, DbCollection);
    var query = coll.AsDiscriminable();
    await InsertDocuments(coll, new [] {
      new Page { _Id = Guid.NewGuid().ToString(), text = "0" },
      new Page { _Id = Guid.NewGuid().ToString(), text = "1" },
      new Page { _Id = Guid.NewGuid().ToString(), text = "2" },
      new Page { _Id = Guid.NewGuid().ToString(), text = "3" }
    });

    AssertExtensions.AreEqual([ "0", "1" ], await query.Page(2, 0).Select(x => x.text).ToListAsync());
    AssertExtensions.AreEqual([ "2", "3" ], await query.Page(2, 1).Select(x => x.text).ToListAsync());
    AssertExtensions.AreEqual([ "3" ], await query.Page(3, 1).Select(x => x.text).ToListAsync());
    AssertExtensions.AreEqual([], await query.Page(2, 2).Select(x => x.text).ToListAsync());
    AssertExtensions.AreEqual([ "0", "1" ], await query.Page(2, null).Select(x => x.text).ToListAsync());
  }
}