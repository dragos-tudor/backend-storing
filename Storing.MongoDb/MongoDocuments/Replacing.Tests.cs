
namespace Storing.MongoDb;

sealed record Replace
{
  public string Id { get; set; } = default!;
  public string rtext = string.Empty;
}

partial class MongoDbTests
{
  [TestMethod]
  public async Task document__replace__replaced_document ()
  {
    var coll = GetMongoCollection<Replace>(Database, "documents");
    var id = Guid.NewGuid().ToString();

    await InsertDocument(coll, new Replace{ Id = id, rtext = "a" });
    await ReplaceDocument(coll, new Replace{ Id = id, rtext = "2" });

    var actual = await
      coll
      .AsQueryable()
      .FirstOrDefaultAsync(document => document.Id == id);
    Assert.AreEqual("2", actual.rtext);
  }
}