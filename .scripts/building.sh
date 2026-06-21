set -e

CONFIGURATION=${1:-Debug}
WORKSPACE_DIR=/workspaces/backend-storing

cd $WORKSPACE_DIR
dotnet build backend-storing.slnx \
  --configuration $CONFIGURATION \
  --no-restore \
  --no-dependencies
