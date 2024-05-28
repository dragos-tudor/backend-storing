
namespace Storing.MongoDb;

sealed record Delete { public string Id { get; set; } = default!; }

public partial class MongoDbTests
{
  [TestMethod]
  public async Task document__delete__deleted_document ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var coll = GetMongoCollection<Delete>(db);

    await InsertDocument(coll, new () { Id = id });
    await DeleteDocument(coll, new () { Id = id });

    var actual = await coll
      .AsQueryable()
      .FirstOrDefaultAsync(document => document.Id == id);
    Assert.IsNull(actual);
  }
}