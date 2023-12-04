using static Storing.MongoDb.MongoCommands;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

public static partial class MongoUsers {

  public static RevokeRolesFromUserCommand GetRevokeRolesFromUserCommand (
    string userName,
    IEnumerable<UserRole> roles,
    WriteConcern? writeConcern = null) =>
      new ( new BsonDocument {
        { "revokeRolesFromUser", userName },
        { "roles", new BsonArray(roles.Select(role => new BsonDocument
          { { "role", role.role} , { "db",  role.db } }))
        },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static RevokeRolesFromUserCommand GetRevokeRolesFromUserCommand (
    string userName,
    IEnumerable<string> roles,
    WriteConcern? writeConcern = null) =>
      new ( new BsonDocument {
        { "revokeRolesFromUser", userName },
        { "roles", new BsonArray(roles) },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static Task<BsonDocument> RevokeRolesFromUser (
    IMongoDatabase db,
    RevokeRolesFromUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default) =>
      RunCommandAsync<RevokeRolesFromUserCommand, BsonDocument> (db, command, readPreference, cancellationToken);

}