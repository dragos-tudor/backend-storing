
using static System.String;

namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  static MongoCredential? CreateMongoCredential(string? user, string? password, bool? ssl, string? defaultDatabase) =>
    (IsNullOrEmpty(user), IsNullOrEmpty(password), ssl) switch {
      (true, true, _) => MongoCredential.CreatePlainCredential(defaultDatabase, user, password),
      (true, _, true) => MongoCredential.CreateMongoX509Credential(user),
      (_, _, _) => default
    };
}