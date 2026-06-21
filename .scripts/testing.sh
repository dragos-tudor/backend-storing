set -e

CONFIGURATION=${1:-Debug}
WORKSPACE_DIR=/workspaces/backend-storing

cd $WORKSPACE_DIR
dotnet test --solution backend-storing.slnx \
  --configuration $CONFIGURATION \
  --no-restore \
  --no-build \
  --verbosity minimal



