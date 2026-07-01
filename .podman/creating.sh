# set -e

# reduce dev container size [use mounted host folder]
PODMAN_ROOT=/workspaces/backend-storing/.containers
[[ -d $PODMAN_ROOT ]] || mkdir -p $PODMAN_ROOT

echo "pull image and run SqlServer container"
podman pull mcr.microsoft.com/mssql/server:2022-latest;
podman run -e MSSQL_SA_PASSWORD=$MSSQL_SA_PASSWORD -e ACCEPT_EULA=Y \
	--network host -d --name sql mcr.microsoft.com/mssql/server:2022-latest;

echo "pull image and run Mongo container"
chmod 400 .podman/mongo-keyfile # IMPORTANT
podman pull docker.io/library/mongo:8.2-noble;
podman run -e MONGO_INITDB_ROOT_USERNAME=$MONGO_INITDB_ROOT_USERNAME -e MONGO_INITDB_ROOT_PASSWORD=$MONGO_INITDB_ROOT_PASSWORD \
	-v .podman/mongo-keyfile:/data/configdb/mongo-keyfile  \
	--network host -d --name mongo docker.io/library/mongo:8.2-noble \
	mongod --bind_ip_all --keyFile /data/configdb/mongo-keyfile --auth;

echo "pull image and run Redis container"
podman pull docker.io/library/redis:8.6.4;
podman run --network host -d --name redis docker.io/library/redis:8.6.4;



