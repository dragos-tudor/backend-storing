using static Storing.MongoDb.MongoCommands;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

public static partial class MongoUsers {

  public static GrantRolesToUserCommand GetGrantRolesToUserCommand (
    string userName,
    IEnumerable<UserRole> roles,
    WriteConcern? writeConcern = null) =>
      new GrantRolesToUserCommand ( new BsonDocument {
        { "grantRolesToUser", userName },
        { "roles", new BsonArray(roles.Select(role => new BsonDocument
          { { "role", role.role} , { "db",  role.db } }))
        },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static GrantRolesToUserCommand GetGrantRolesToUserCommand (
    string userName,
    IEnumerable<string> roles,
    WriteConcern? writeConcern = null) =>
      new GrantRolesToUserCommand ( new BsonDocument {
        { "grantRolesToUser", userName },
        { "roles", new BsonArray(roles) },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static Task<BsonDocument> GrantRolesToUser (
    IMongoDatabase db,
    GrantRolesToUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken token = default) =>
      RunCommandAsync<GrantRolesToUserCommand, BsonDocument> (db, command, readPreference, token);

}