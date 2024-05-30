namespace Docker.Extensions;

partial class DockerFuncs
{
  static string GetFromImage(string image) => image.Split(':')[0];

  static string? GetImageTag(string image) => image.Contains(':', StringComparison.Ordinal)? image.Split(':')[1]: default;

  public static ImagesListResponse? GetImage (IEnumerable<ImagesListResponse> imageList, string imageName) =>
    imageList.FirstOrDefault(image => image.RepoTags.Any(repoTag => repoTag == imageName));
}