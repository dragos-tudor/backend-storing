using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

record Delete: Id<string> { }

public partial class MongoDocumentsTests {

  [Fact]
  internal async Task document__delete__deleted_document ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Delete>(db, dbCollection);

    await InsertDocument(coll, (new Delete{ _id = id }));
    await DeleteDocument(coll, (new Delete{ _id = id }));

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(document => document._id == id);
    Assert.Null(actual);
  }

}