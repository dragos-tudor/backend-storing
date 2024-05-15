// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using StackExchange.Redis;
using StackExchange.Redis.Profiling;

namespace Storing.Redis;

public partial class RedisOptions
{
  public ConfigurationOptions ConfigurationOptions { get; internal set; } = default!;
  public string? InstanceName { get; internal set; }
  public Func<ProfilingSession>? ProfilingSession { get; internal set; }
}
