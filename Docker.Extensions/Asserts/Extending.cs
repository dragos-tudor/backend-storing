
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docker.Extensions;

public static class AssertExtensions
{
  public static void AreEqual<T>(
    IEnumerable<T> expected,
    IEnumerable<T>? actual)
  =>
    CollectionAssert.AreEqual(expected.ToArray(), actual?.ToArray());

  public static void IsSubsetOf<T>(
    IEnumerable<T> expected,
    IEnumerable<T>? actual)
  =>
    CollectionAssert.IsSubsetOf(expected.ToArray(), actual?.ToArray());
}