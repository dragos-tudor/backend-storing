namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static void MapClassType<T> (BsonClassMap<T> classMap) {
    classMap.AutoMap();
    classMap.SetIgnoreExtraElements(true);
    classMap.Freeze();
  }

  public static void MapDiscriminatorType<T> (BsonClassMap<T> classMap) {
    classMap.AutoMap();
    classMap.SetDiscriminator(typeof(T).Name);
    classMap.SetDiscriminatorIsRequired(true);
    classMap.SetIgnoreExtraElements(true);
    classMap.Freeze();
  }
}