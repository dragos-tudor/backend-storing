set -e

CONFIGURATION=${1:-Debug}
WORKSPACE_DIR=.
PROJECTS=(
  "Storing.MongoDb"
  "Storing.Redis"
  "Storing.SqlServer"
)

dotnet restore
for PROJECT in ${PROJECTS[@]}; do
  echo "building project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet build --no-restore --no-dependencies -c $CONFIGURATION && cd ..
done
