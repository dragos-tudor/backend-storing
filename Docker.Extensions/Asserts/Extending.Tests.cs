
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docker.Extensions;

partial class DockerFuncs
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