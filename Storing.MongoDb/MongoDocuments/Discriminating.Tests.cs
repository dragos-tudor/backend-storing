using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoCollections;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

[BsonDiscriminator(discriminator: nameof(Discriminated), Required = true)]
record Discriminated: Id<string> { }

record NonDiscriminated: Id<string> { }

public partial class MongoDocumentsTests {

  [Fact]
  internal async Task discriminated_documents_coll__query_as_discriminable__discriminated_documents ()
  {
    var db = await GetMongoDatabase();
    var discriminatedColl = GetCollection<Discriminated>(db, dbCollection);
    var nonDiscriminatedColl = GetCollection<NonDiscriminated>(db, dbCollection);
    var discriminated = new [] {
      new Discriminated { _id = Guid.NewGuid().ToString() },
      new Discriminated { _id = Guid.NewGuid().ToString() }
    };
    var nonDiscriminated = new [] {
      new NonDiscriminated { _id = Guid.NewGuid().ToString() },
    };

    await InsertDocuments(discriminatedColl, discriminated);
    await InsertDocuments(nonDiscriminatedColl, nonDiscriminated);

    var actual = await discriminatedColl.AsDiscriminable().CountAsync();
    Assert.Equal(2, actual);
  }

}