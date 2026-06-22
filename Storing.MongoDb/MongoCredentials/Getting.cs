
using System.Net;

namespace Storing.MongoDb;

partial class MongoDbFuncs
{
    static string GetMongoCredentials(string? userName, string? password) =>
      !(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)) ?
        $"{WebUtility.UrlEncode(userName)}:{WebUtility.UrlEncode(password)}" :
        string.Empty;
}