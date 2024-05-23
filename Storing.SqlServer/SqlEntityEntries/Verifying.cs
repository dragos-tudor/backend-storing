using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  static bool IsEntityLinkEquality<T> (
    EntityEntry entry,
    string keyName,
    T keyValue)
  {
    var dictEntry = (Dictionary<string, object>) entry.Entity;
    var dictValue = dictEntry.GetValueOrDefault(keyName);

    return dictValue != default?
      dictValue.ToString()! == keyValue!.ToString():
      false;
  }

  static bool IsEntitiesLink (object @object) =>
    @object.GetType().IsAssignableTo(
      typeof(Dictionary<string, object>));
}