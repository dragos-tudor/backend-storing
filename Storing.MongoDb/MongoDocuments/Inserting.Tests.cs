
namespace Storing.MongoDb;

sealed record Insert: Id<string> { }

public partial class MongoDocumentsTests
{
  [TestMethod]
  public async Task document__insert__inserted_document ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Insert>(db, DbCollection);

    await InsertDocument(coll, new Insert{ _Id = id });

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(x => x._Id == id);
    Assert.IsNotNull(actual);
  }
}