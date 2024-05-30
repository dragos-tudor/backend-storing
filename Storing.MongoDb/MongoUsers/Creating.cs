
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static Task<BsonDocument> CreateUser (
    IMongoDatabase db,
    CreateUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default) =>
      RunCommandAsync<CreateUserCommand, BsonDocument> (db, command, readPreference, cancellationToken);

  public static Task<BsonDocument> CreateUserForDatabases (
    IMongoDatabase db,
    CreateUserForDatabasesCommand command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default) =>
      RunCommandAsync<CreateUserForDatabasesCommand, BsonDocument> (db, command, readPreference, cancellationToken);

  public static CreateUserCommand CreateCreateUserCommand (
    string userName,
    string password,
    IEnumerable<string> roles,
    WriteConcern? writeConcern = null) =>
      new ( new BsonDocument {
        { "createUser", userName },
        { "pwd", password },
        { "roles", new BsonArray(roles) },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static CreateUserForDatabasesCommand CreateCreateUserForDatabasesCommand (
    string userName,
    string password,
    IEnumerable<UserRole> roles,
    WriteConcern? writeConcern = null) =>
      new ( new BsonDocument {
        { "createUser", userName },
        { "pwd", password },
        { "roles", new BsonArray(roles.Select(ToBsonDocument)) },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });
}