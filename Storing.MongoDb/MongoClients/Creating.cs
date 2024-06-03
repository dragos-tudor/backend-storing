namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static MongoClient CreateMongoClient (string connString, Func<MongoUrlBuilder, MongoUrlBuilder>? setUrlBuilder = null) =>
    setUrlBuilder is null?
      new (new MongoUrlBuilder(connString).ToMongoUrl()):
      new (setUrlBuilder(new MongoUrlBuilder(connString)).ToMongoUrl());
}