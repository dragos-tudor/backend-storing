namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static MongoClient CreateMongoClient (
    string connection,
    Func<MongoUrlBuilder, MongoUrlBuilder>? setUrlBuilder = null)
  {
    var urlBuilder = new MongoUrlBuilder(connection);
    return (setUrlBuilder is null) switch {
      true =>  new MongoClient(urlBuilder.ToMongoUrl()),
      false => new MongoClient(setUrlBuilder(urlBuilder).ToMongoUrl())
    };
  }

  public static MongoClient CreateMongoClient (
    string connection,
    string userName,
    string password,
    Func<MongoUrlBuilder, MongoUrlBuilder>? setUrlBuilder = null)
  {
    var urlBuilder = new MongoUrlBuilder(connection) {
      Username = userName,
      Password = password
    };

    return (setUrlBuilder is null) switch {
      true =>  new MongoClient(urlBuilder.ToMongoUrl()),
      false => new MongoClient(setUrlBuilder(urlBuilder).ToMongoUrl())
    };
  }
}