set -e

CONFIGURATION=${1:-Debug}

cd $WORKSPACE_ROOT
dotnet test --solution backend-storing.slnx \
  --configuration $CONFIGURATION \
  --no-restore \
  --no-build \
  --verbosity minimal



