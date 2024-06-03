
namespace Storing.MongoDb;

sealed record Insert { public string Id { get; set; } = default!; }

partial class MongoDbTests
{
  [TestMethod]
  public async Task document__insert__inserted_document ()
  {
    var coll = GetMongoCollection<Insert>(Database, "documents");
    var id = Guid.NewGuid().ToString();

    await InsertDocument(coll, new Insert{ Id = id });

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(x => x.Id == id);
    Assert.IsNotNull(actual);
  }
}