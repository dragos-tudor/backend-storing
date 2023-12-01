using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoDocuments;
using static Storing.MongoDb.MongoCollections;

namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Page), Required = true)]
record Page : Id<string> {
  public string text = string.Empty;
}

public partial class MongoQueriesTests {

  [Fact]
  internal async Task documents__page__paged_documents ()
  {
    var db = await GetMongoDatabase();
    var coll = GetCollection<Page>(db, dbCollection);
    var query = coll.AsDiscriminable();
    await InsertDocuments(coll, new [] {
      new Page { _id = Guid.NewGuid().ToString(), text = "0" },
      new Page { _id = Guid.NewGuid().ToString(), text = "1" },
      new Page { _id = Guid.NewGuid().ToString(), text = "2" },
      new Page { _id = Guid.NewGuid().ToString(), text = "3" }
    });

    Assert.Equal(new []{ "0", "1" }, await query.Page(2, 0).Select(x => x.text).ToListAsync());
    Assert.Equal(new []{ "2", "3" }, await query.Page(2, 1).Select(x => x.text).ToListAsync());
    Assert.Equal(new []{ "3" }, await query.Page(3, 1).Select(x => x.text).ToListAsync());
    Assert.Equal(new string []{ }, await query.Page(2, 2).Select(x => x.text).ToListAsync());
    Assert.Equal(new []{ "0", "1" }, await query.Page(2, null).Select(x => x.text).ToListAsync());
  }

}