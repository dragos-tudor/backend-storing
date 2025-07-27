set -e

WORKSPACE_DIR=.
PROJECTS=(
  "Storing.MongoDb"
  "Storing.Redis"
  "Storing.SqlServer"
)

./building.sh Debug
for PROJECT in ${PROJECTS[@]}; do
  echo "testing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet run --no-build --no-restore -- --settings ../.runsettings ** cd ..
done
