
namespace Storing.MongoDb;

sealed record Replace: Id<string>
{
  public string rtext = string.Empty;
}

partial class MongoDbTests
{
  [TestMethod]
  public async Task document__replace__replaced_document ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetCollection<Replace>(db, DbCollection);

    await InsertDocument(coll, new Replace{ _Id = id, rtext = "a" });
    await ReplaceDocument(coll, new Replace{ _Id = id, rtext = "2" });

    var actual = await
      coll
      .AsQueryable()
      .FirstOrDefaultAsync(document => document._Id == id);
    Assert.AreEqual("2", actual.rtext);
  }
}