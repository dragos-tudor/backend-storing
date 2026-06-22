
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static MongoClientSettings GetMongoClientSettings(
    IEnumerable<string> endpoints,
    string? user = default,
    string? password = default,
    string? defaultDatabase = default,
    bool ssl = false,
    bool sslCertificateValidation = false,
    TimeSpan? connectTimeout = default,
    string? replicaSet = default,
    Action<MongoClientSettings>? configBuilder = default)
  =>
    SetMongoClientSettings(
      new MongoClientSettings()
      {
        Servers = endpoints.Select(endpoint => MongoServerAddress.Parse(endpoint)),
        Credential = CreateMongoCredential(user, password, ssl, defaultDatabase),
        ConnectTimeout = connectTimeout?? TimeSpan.FromSeconds(15),
        ReplicaSetName = replicaSet,
        UseTls = ssl,
        AllowInsecureTls = sslCertificateValidation,
      },
      configBuilder
    );


  public static MongoClientSettings GetMongoClientSettings(MongoOptions options) =>
    GetMongoClientSettings(
      options.EndPoints, options.User,
      options.Password, options.DefaultDatabase,
      options.Ssl, options.SslCertificateValidation,
      options.ConnectTimeout, options.ReplicaSet);
}