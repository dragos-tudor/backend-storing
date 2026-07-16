set -eu

VERSION=${1}

cd $WORKSPACE_ROOT
mkdir -p ./.packages
for PROJECT in $(dotnet sln list)
do
  [ -e "$PROJECT" ] || continue
  ./.scripts/packing.sh "$PROJECT" "$VERSION"
done

if [ -x "$(command -v dotnet-validate)" ]; then
  for PACKAGE in $(ls .packages | xargs -0 -- basename | grep -E .+[.]nupkg)
  do
    dotnet-validate package local .packages/$PACKAGE
  done
fi