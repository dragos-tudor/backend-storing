using static Storing.MongoDb.MongoCommands;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

public static partial class MongoUsers {

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

  public static CreateUserCommand GetCreateUserCommand (
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

  public static CreateUserForDatabasesCommand GetCreateUserForDatabasesCommand (
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