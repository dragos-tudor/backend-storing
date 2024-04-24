using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

sealed record Delete: Id<string> { }

[TestClass]
public partial class MongoDocumentsTests
{
  [TestMethod]
  public async Task document__delete__deleted_document ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Delete>(db, DbCollection);

    await InsertDocument(coll, new () { _Id = id });
    await DeleteDocument(coll, new () { _Id = id });

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(document => document._Id == id);
    Assert.IsNull(actual);
  }
}