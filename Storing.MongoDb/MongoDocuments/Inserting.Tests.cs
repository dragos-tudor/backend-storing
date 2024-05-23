
namespace Storing.MongoDb;

sealed record Insert: Id<string> { }

partial class MongoDbTests
{
  [TestMethod]
  public async Task document__insert__inserted_document ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Insert>(db);

    await InsertDocument(coll, new Insert{ _Id = id });

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(x => x._Id == id);
    Assert.IsNotNull(actual);
  }
}