
namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Order), Required = true)]
sealed record Order : Id<string>
{
  public string text = string.Empty;
}

partial class MongoDbTests
{
  [TestMethod]
  public async Task unordered_documents__order__ordered_documents ()
  {
    var db = GetMongoDatabase();
    var coll = GetMongoCollection<Order>(db);
    var query = coll.AsDiscriminable();
    await InsertDocuments(coll, new [] {
      new Order { _Id = Guid.NewGuid().ToString(), text = "1" },
      new Order { _Id = Guid.NewGuid().ToString(), text = "0" },
      new Order { _Id = Guid.NewGuid().ToString(), text = "3" },
      new Order { _Id = Guid.NewGuid().ToString(), text = "2" }
    });

    Expression<Func<Order, string>> exp = x => x.text;
    AreEqual([ "0", "1", "2", "3" ], await query.Order(true, exp).Select(exp).ToListAsync());
    AreEqual([ "3", "2", "1", "0" ], await query.Order(false, exp).Select(exp).ToListAsync());
    AreEqual([ "1", "0", "3", "2" ], await query.Order(null, exp).Select(exp).ToListAsync());
  }
}