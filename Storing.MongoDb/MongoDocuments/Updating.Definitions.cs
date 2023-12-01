namespace Storing.MongoDb;

public partial class MongoDocumentsTests {

  static readonly ConcurrentDictionary<string, object> UpdateBuilders =
    new ConcurrentDictionary<string, object>();

  static UpdateDefinitionBuilder<T> GetUpdateBuilder<T> () =>
    ((UpdateDefinitionBuilder<T>)UpdateBuilders
      .GetOrAdd(typeof(T).Name, (key) =>
        new UpdateDefinitionBuilder<T>()));

  public static UpdateDefinition<T1> AddToSetEachDefinition<T1, T2> (
    FieldDefinition<T1, T2> field,
    IEnumerable<T2> items) =>
      GetUpdateBuilder<T1>().AddToSetEach(field, items);

  public static UpdateDefinition<T> CombineDefinitions<T> (params UpdateDefinition<T>[] definitions) =>
    GetUpdateBuilder<T>().Combine(definitions);

  public static UpdateDefinition<T1> PullAllDefinition<T1, T2> (
    FieldDefinition<T1, T2> field,
    IEnumerable<T2> items) =>
      GetUpdateBuilder<T1>().PullAll(field, items);

  public static UpdateDefinition<T1> SetFieldDefinition<T1, T2> (
    FieldDefinition<T1, T2> field,
    T2 value) =>
      GetUpdateBuilder<T1>().Set(field, value);

}