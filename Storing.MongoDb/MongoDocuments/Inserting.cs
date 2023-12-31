using static Storing.MongoDb.MongoIdentities;

namespace Storing.MongoDb;

public static partial class MongoDocuments {

  public static Task InsertDocument<T> (
    IMongoCollection<T> collection,
    T document,
    InsertOneOptions? options = null,
    CancellationToken cancellationToken = default)
    where T: Id =>
      collection.InsertOneAsync(document, options, cancellationToken);

  public static Task InsertDocuments<T> (
    IMongoCollection<T> collection,
    IEnumerable<T> documents,
    InsertManyOptions? options = null,
    CancellationToken cancellationToken = default)
    where T: Id =>
      collection.InsertManyAsync(documents, options, cancellationToken);


}