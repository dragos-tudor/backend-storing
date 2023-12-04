using static Storing.MongoDb.MongoIdentities;

namespace Storing.MongoDb;

public static partial class MongoDocuments {

  public static async Task<DeleteResult> DeleteDocument<T> (
    IMongoCollection<T> collection,
    T document,
    DeleteOptions? options = null,
    CancellationToken cancellationToken = default)
    where T: Id {
      var filter = GetIdFilterDefinition<T>(document);
      return await collection.DeleteOneAsync(filter, options, cancellationToken);
    }

}