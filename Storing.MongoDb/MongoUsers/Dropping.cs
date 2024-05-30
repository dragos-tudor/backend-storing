
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static Task<BsonDocument> DropUser (
    IMongoDatabase db,
    DropUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default) =>
      RunCommandAsync<DropUserCommand, BsonDocument> (db, command, readPreference, cancellationToken);

  public static BsonDocument DropAllUsers (
    IMongoDatabase db,
    DropAllUsersCommand command,
    CancellationToken cancellationToken = default) =>
      RunCommand<DropAllUsersCommand, BsonDocument> (db, command, null, cancellationToken);

  public static DropUserCommand CreateDropUserCommand (string userName, WriteConcern? writeConcern = null) =>
    new ( new BsonDocument {
      { "dropUser", userName },
      { "writeConcern", ToBsonDocument(writeConcern) }
    });

  public static DropAllUsersCommand CreateDropAllUsersCommand (WriteConcern? writeConcern = null) =>
    new ( new BsonDocument {
      { "dropAllUsersFromDatabase", 1 },
      { "writeConcern", ToBsonDocument(writeConcern) }
    });
}