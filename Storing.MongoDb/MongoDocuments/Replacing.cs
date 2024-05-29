
namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static Task ReplaceDocument<T> (
    IMongoCollection<T> collection,
    T document,
    ReplaceOptions? options = null,
    CancellationToken cancellationToken = default)
  {
    var filter = GetIdFilterDefinition<T>(document);
    return collection.ReplaceOneAsync(filter, document, options, cancellationToken);
  }

  public static Task ReplaceDocument<T> (
    IClientSessionHandle session,
    IMongoCollection<T> collection,
    T document,
    ReplaceOptions? options = null,
    CancellationToken cancellationToken = default)
  {
    var filter = GetIdFilterDefinition<T>(document);
    return collection.ReplaceOneAsync(session, filter, document, options, cancellationToken);
  }
}