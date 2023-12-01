namespace Storing.MongoDb;

public static partial class MongoIdentities {

  static BsonMemberMap GetIdMemberMap<T>() =>
    BsonClassMap.LookupClassMap(typeof(T)).IdMemberMap;

  public static FilterDefinition<T> GetIdFilterDefinition<T> (T document) {
    var idMemberMap = GetIdMemberMap<T>();
    var fieldValue = idMemberMap.Getter.Invoke(document);
    var fieldDefinition = idMemberMap.ElementName;

    return new FilterDefinitionBuilder<T>()
      .Eq(fieldDefinition, fieldValue);
  }

}



