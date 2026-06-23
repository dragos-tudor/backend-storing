namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  [Obsolete("Use CreateMongoClient with MongoClientSettings instead")]
  public static MongoClient CreateMongoClient (string connString, Func<MongoUrlBuilder, MongoUrlBuilder>? setBuilder = null) =>
    setBuilder is null?
      new (new MongoUrlBuilder(connString).ToMongoUrl()):
      new (setBuilder(new MongoUrlBuilder(connString)).ToMongoUrl());

  public static MongoClient CreateMongoClient (MongoClientSettings settings, Action<MongoClientSettings>? configBuilder = null) =>
    configBuilder is null?
      new (settings):
      new (SetMongoClientSettings(settings, configBuilder));
}