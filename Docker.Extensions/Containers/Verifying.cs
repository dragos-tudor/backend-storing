namespace Docker.Extensions;

partial class DockerFuncs
{
  static bool IsExistingContainer<T>(T? container) where T: class => container is not null;

  static bool IsPausedContainer(ContainerState state) => state.Paused;

  static bool IsRunningContainer(ContainerState state) => state.Running;

  static bool IsKilledContainer(ContainerState state) => state.OOMKilled;

  static bool IsDeadContainer(ContainerState state) => state.Dead;
}