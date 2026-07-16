set -eu

PROJECT=${1}
VERSION=${2}

dotnet pack \
  --configuration Release \
  --output "${WORKSPACE_ROOT}/.packages" \
  -p:PackOnly=true \
  -p:Version="${VERSION}" \
  -p:PackageVersion="${VERSION}" \
  $PROJECT
