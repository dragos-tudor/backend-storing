
namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Order), Required = true)]
sealed record Order
{
  public string Id { get; set; } = default!;
  public string text = string.Empty;
}

partial class MongoDbTests
{
  [TestMethod]
  public async Task unordered_documents__order__ordered_documents ()
  {
    var coll = GetMongoCollection<Order>(Database, "queries");
    var query = coll.AsDiscriminable();
    await InsertDocuments(coll, [
      new Order { Id = Guid.NewGuid().ToString(), text = "1" },
      new Order { Id = Guid.NewGuid().ToString(), text = "0" },
      new Order { Id = Guid.NewGuid().ToString(), text = "3" },
      new Order { Id = Guid.NewGuid().ToString(), text = "2" }
    ]);

    Expression<Func<Order, string>> exp = x => x.text;
    (await query.Order(true, exp).Select(exp).ToListAsync()).ShouldBe([ "0", "1", "2", "3" ]);
    (await query.Order(false, exp).Select(exp).ToListAsync()).ShouldBe([ "3", "2", "1", "0" ]);
    (await query.Order(null, exp).Select(exp).ToListAsync()).ShouldBe([ "1", "0", "3", "2" ]);
  }
}