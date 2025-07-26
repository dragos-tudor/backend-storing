global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Linq.Expressions;
global using System.Collections.Generic;
global using Microsoft.EntityFrameworkCore;
global using static Storing.SqlServer.SqlServerFuncs;

namespace Storing.SqlServer;

public static partial class SqlServerFuncs;

#if RELEASE
  public static class Program { public static void Main() {} }
#endif

