
namespace Storing.MongoDb;

sealed record Delete: Id<string> { }

public partial class MongoDbTests
{
  [TestMethod]
  public async Task document__delete__deleted_document ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Delete>(db);

    await InsertDocument(coll, new () { _Id = id });
    await DeleteDocument(coll, new () { _Id = id });

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(document => document._Id == id);
    Assert.IsNull(actual);
  }
}