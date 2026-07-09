set -e

CONFIGURATION=${1:-Debug}

cd $WORKSPACE_ROOT
dotnet build backend-storing.slnx \
  --configuration $CONFIGURATION \
  --no-restore \
  --no-dependencies
