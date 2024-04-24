using static Storing.MongoDb.MongoIdentities;

namespace Storing.MongoDb;

public static partial class MongoDocuments
{
  public static Task ReplaceDocument<T> (
    IMongoCollection<T> collection,
    T document,
    ReplaceOptions? options = null,
    CancellationToken cancellationToken = default)
    where T: Id {
      var filter = GetIdFilterDefinition<T>(document);
      return collection.ReplaceOneAsync(filter, document, options, cancellationToken);
    }
}