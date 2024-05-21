
namespace Storing.MongoDb;

[BsonDiscriminator(discriminator: nameof(Discriminated), Required = true)]
sealed record Discriminated: Id<string> { }

sealed record NonDiscriminated: Id<string> { }

partial class MongoDbTests
{
  [TestMethod]
  public async Task discriminated_documents_coll__query_as_discriminable__discriminated_documents ()
  {
    var db = GetMongoDatabase();
    var discriminatedColl = GetCollection<Discriminated>(db, DbCollection);
    var nonDiscriminatedColl = GetCollection<NonDiscriminated>(db, DbCollection);
    var discriminated = new [] {
      new Discriminated { _Id = Guid.NewGuid().ToString() },
      new Discriminated { _Id = Guid.NewGuid().ToString() }
    };
    var nonDiscriminated = new [] {
      new NonDiscriminated { _Id = Guid.NewGuid().ToString() },
    };

    await InsertDocuments(discriminatedColl, discriminated);
    await InsertDocuments(nonDiscriminatedColl, nonDiscriminated);

    var actual = await discriminatedColl.AsDiscriminable().CountAsync();
    Assert.AreEqual(2, actual);
  }
}