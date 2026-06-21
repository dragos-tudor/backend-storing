set -eu

PROJECT=${1}
VERSION=${2}
WORKSPACE_DIR=/workspaces/backend-storing

dotnet pack $PROJECT \
  --configuration Release \
  --output "${WORKSPACE_DIR}/.packages" \
  -p:PackOnly=true \
  -p:Version="${VERSION}" \
  -p:PackageVersion="${VERSION}"

