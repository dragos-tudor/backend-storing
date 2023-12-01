using static Storing.MongoDb.MongoCommands;
using static Storing.MongoDb.MongoDocuments;

namespace Storing.MongoDb;

public static partial class MongoUsers {

  public static Task<BsonDocument> DropUser (
    IMongoDatabase db,
    DropUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken token = default) =>
      RunCommandAsync<DropUserCommand, BsonDocument> (db, command, readPreference, token);

  public static BsonDocument DropAllUsers (
    IMongoDatabase db,
    DropAllUsersCommand command,
    CancellationToken token = default) =>
      RunCommand<DropAllUsersCommand, BsonDocument> (db, command, null, token);

  public static DropUserCommand GetDropUserCommand (string userName, WriteConcern? writeConcern = null) =>
    new DropUserCommand ( new BsonDocument {
      { "dropUser", userName },
      { "writeConcern", ToBsonDocument(writeConcern) }
    });

  public static DropAllUsersCommand GetDropAllUsersCommand (WriteConcern? writeConcern = null) =>
    new DropAllUsersCommand ( new BsonDocument {
      { "dropAllUsersFromDatabase", 1 },
      { "writeConcern", ToBsonDocument(writeConcern) }
    });

}