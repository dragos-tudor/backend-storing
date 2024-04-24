using static Storing.MongoDb.MongoCommands;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

public static partial class MongoUsers
{
  public static GrantRolesToUserCommand CreateGrantRolesToUserCommand (
    string userName,
    IEnumerable<UserRole> roles,
    WriteConcern? writeConcern = null) =>
      new ( new BsonDocument {
        { "grantRolesToUser", userName },
        { "roles", new BsonArray(roles.Select(role => new BsonDocument
          { { "role", role.Role} , { "db",  role.Db } }))
        },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static GrantRolesToUserCommand CreateGrantRolesToUserCommand (
    string userName,
    IEnumerable<string> roles,
    WriteConcern? writeConcern = null) =>
      new ( new BsonDocument {
        { "grantRolesToUser", userName },
        { "roles", new BsonArray(roles) },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static Task<BsonDocument> GrantRolesToUser (
    IMongoDatabase db,
    GrantRolesToUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default) =>
      RunCommandAsync<GrantRolesToUserCommand, BsonDocument> (db, command, readPreference, cancellationToken);
}