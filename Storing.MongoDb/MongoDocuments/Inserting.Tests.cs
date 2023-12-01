using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

record Insert: Id<string> { }

public partial class MongoDocumentsTests {

  [Fact]
  internal async Task document__insert__inserted_document ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Insert>(db, dbCollection);

    await InsertDocument(coll, (new Insert{ _id = id }));

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(x => x._id == id);
    Assert.NotNull(actual);
  }

}