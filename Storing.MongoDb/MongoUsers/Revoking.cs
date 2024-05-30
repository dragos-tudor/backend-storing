
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static RevokeRolesFromUserCommand CreateRevokeRolesFromUserCommand (
    string userName,
    IEnumerable<UserRole> roles,
    WriteConcern? writeConcern = null) =>
      new ( new BsonDocument {
        { "revokeRolesFromUser", userName },
        { "roles", new BsonArray(roles.Select(role => new BsonDocument
          { { "role", role.Role} , { "db",  role.Db } }))
        },
        { "writeConcern", ToBsonDocument(writeConcern) }
      });

  public static RevokeRolesFromUserCommand CreateRevokeRolesFromUserCommand (
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