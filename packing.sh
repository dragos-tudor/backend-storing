set -e

WORKSPACE_DIR=.
PROJECTS=(
  "Storing.MongoDb"
  "Storing.Redis"
  "Storing.SqlServer"
)

./building.sh Release
for PROJECT in ${PROJECTS[@]}; do
  echo "packing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet msbuild -t:Packing && cd ..
done
