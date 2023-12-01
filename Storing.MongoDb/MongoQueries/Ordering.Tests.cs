using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoDocuments;
using static Storing.MongoDb.MongoCollections;

namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Order), Required = true)]
record Order : Id<string> {
  public string text = String.Empty;
}

public partial class MongoQueriesTests {

  [Fact]
  internal async Task unordered_documents__order__ordered_documents ()
  {
    var db = await GetMongoDatabase();
    var coll = GetCollection<Order>(db, dbCollection);
    var query = coll.AsDiscriminable();
    await InsertDocuments(coll, new [] {
      new Order { _id = Guid.NewGuid().ToString(), text = "1" },
      new Order { _id = Guid.NewGuid().ToString(), text = "0" },
      new Order { _id = Guid.NewGuid().ToString(), text = "3" },
      new Order { _id = Guid.NewGuid().ToString(), text = "2" }
    });

    Expression<Func<Order, string>> exp = x => x.text;
    Assert.Equal(new []{ "0", "1", "2", "3" }, await query.Order(true, exp).Select(exp).ToListAsync());
    Assert.Equal(new []{ "3", "2", "1", "0" }, await query.Order(false, exp).Select(exp).ToListAsync());
    Assert.Equal(new []{ "1", "0", "3", "2" }, await query.Order(null, exp).Select(exp).ToListAsync());
  }

}