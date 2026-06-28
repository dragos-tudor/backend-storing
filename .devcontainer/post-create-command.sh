# set -e


has_container() {
  podman ps --format "{{.Names}}" -a | grep -q "$1"
}

echo "pull image and run SqlServer container"
podman pull mcr.microsoft.com/mssql/server:2022-latest;
if ! has_container "storing-sql"; then
	podman run -e MSSQL_SA_PASSWORD=$MSSQL_SA_PASSWORD -e ACCEPT_EULA=Y \
		--network host -d --name storing-sql mcr.microsoft.com/mssql/server:2022-latest;
fi

echo "pull image and run Mongo container"
chmod 400 .devcontainer/mongo-keyfile # IMPORTANT
podman pull docker.io/library/mongo:8.2-noble;
if ! has_container "storing-mongo"; then
	podman run -e MONGO_INITDB_ROOT_USERNAME=$MONGO_INITDB_ROOT_USERNAME -e MONGO_INITDB_ROOT_PASSWORD=$MONGO_INITDB_ROOT_PASSWORD \
		-v .devcontainer/mongo-keyfile:/data/configdb/mongo-keyfile  \
		--network host -d --name storing-mongo docker.io/library/mongo:8.2-noble \
		mongod --bind_ip_all --keyFile /data/configdb/mongo-keyfile --auth;
fi

echo "pull image and run Redis container"
podman pull docker.io/library/redis:8.6.4;
if ! has_container "storing-redis"; then
	podman run --network host -d --name storing-redis docker.io/library/redis:8.6.4;
fi


