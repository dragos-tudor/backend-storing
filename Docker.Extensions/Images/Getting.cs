namespace Docker.Extensions;

partial class Images
{
  static string GetFromImage(string image) => image.Split(":")[0];

  static string? GetImageTag(string image) => image.Contains(":")? image.Split(":")[1]: default;

}