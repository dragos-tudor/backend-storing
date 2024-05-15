
namespace Storing.MongoDb;

partial class MongoFuncs
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