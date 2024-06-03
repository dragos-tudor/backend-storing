
namespace Storing.MongoDb;

[BsonDiscriminator(discriminator: nameof(Discriminated), Required = true)]
sealed record Discriminated { public string Id { get; set; } = default!; }

sealed record NonDiscriminated { public string Id { get; set; } = default!; }

partial class MongoDbTests
{
  [TestMethod]
  public async Task discriminated_documents_coll__query_as_discriminable__discriminated_documents ()
  {
    var discriminatedColl = GetMongoCollection<Discriminated>(Database, "documents");
    var nonDiscriminatedColl = GetMongoCollection<NonDiscriminated>(Database, "documents");
    var discriminated = new [] {
      new Discriminated { Id = Guid.NewGuid().ToString() },
      new Discriminated { Id = Guid.NewGuid().ToString() }
    };
    var nonDiscriminated = new [] {
      new NonDiscriminated { Id = Guid.NewGuid().ToString() },
    };

    await InsertDocuments(discriminatedColl, discriminated);
    await InsertDocuments(nonDiscriminatedColl, nonDiscriminated);

    var actual = await discriminatedColl.AsDiscriminable().CountAsync();
    Assert.AreEqual(2, actual);
  }
}