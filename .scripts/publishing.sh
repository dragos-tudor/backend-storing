set -eu

PACKAGE=$1
GITHUB_TOKEN=$2
GITHUB_OWNER=$3

dotnet nuget push "${PACKAGE}" \
  --source "https://nuget.pkg.github.com/${GITHUB_OWNER}/index.json" \
  --api-key "${GITHUB_TOKEN}" \
  --skip-duplicate