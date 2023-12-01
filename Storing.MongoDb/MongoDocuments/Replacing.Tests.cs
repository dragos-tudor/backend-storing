using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

record Replace: Id<string> {
  public string rtext = string.Empty;
}

public partial class MongoDocumentsTests {

  [Fact]
  internal async Task document__replace__replaced_document ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Replace>(db, dbCollection);

    await InsertDocument(coll, (new Replace{ _id = id, rtext = "a" }));
    await ReplaceDocument(coll, (new Replace{ _id = id, rtext = "2" }));

    var actual = await
      coll
      .AsQueryable()
      .FirstOrDefaultAsync(document => document._id == id);
    Assert.Equal("2", actual.rtext);
  }

}