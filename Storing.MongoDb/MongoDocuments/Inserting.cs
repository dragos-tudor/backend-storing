
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static Task InsertDocument<T> (
    IMongoCollection<T> collection,
    T document,
    InsertOneOptions? options = null,
    CancellationToken cancellationToken = default)
  =>
    collection.InsertOneAsync(document, options, cancellationToken);

  public static Task InsertDocument<T> (
    IClientSessionHandle session,
    IMongoCollection<T> collection,
    T document,
    InsertOneOptions? options = null,
    CancellationToken cancellationToken = default)
  =>
    collection.InsertOneAsync(session, document, options, cancellationToken);

  public static Task InsertDocuments<T> (
    IMongoCollection<T> collection,
    IEnumerable<T> documents,
    InsertManyOptions? options = null,
    CancellationToken cancellationToken = default)
  =>
    collection.InsertManyAsync(documents, options, cancellationToken);

  public static Task InsertDocuments<T> (
    IClientSessionHandle session,
    IMongoCollection<T> collection,
    IEnumerable<T> documents,
    InsertManyOptions? options = null,
    CancellationToken cancellationToken = default)
  =>
    collection.InsertManyAsync(session, documents, options, cancellationToken);
}