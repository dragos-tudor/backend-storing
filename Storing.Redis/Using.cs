global using System;
global using System.Threading.Tasks;
global using static Storing.Redis.RedisFuncs;

namespace Storing.Redis;

public static partial class RedisFuncs;

#if RELEASE
  public static class Program { public static void Main() {} }
#endif