set -e
set +H

SERVICES_ROOT=$WORKSPACE_ROOT/.services

echo "starting SqlServer container"
podman pull mcr.microsoft.com/mssql/server:2022-latest;
podman run -e MSSQL_SA_PASSWORD=$MSSQL_SA_PASSWORD -e ACCEPT_EULA=Y \
	--network host -d --name sql mcr.microsoft.com/mssql/server:2022-latest;

echo "starting Mongo container"
chmod 400 $SERVICES_ROOT/mongo-keyfile # IMPORTANT
podman pull docker.io/library/mongo:8.2-noble;
podman run -e MONGO_INITDB_ROOT_USERNAME=$MONGO_INITDB_ROOT_USERNAME -e MONGO_INITDB_ROOT_PASSWORD=$MONGO_INITDB_ROOT_PASSWORD \
	-v $SERVICES_ROOT/mongo-keyfile:/data/configdb/mongo-keyfile  \
	--network host -d --name mongo docker.io/library/mongo:8.2-noble \
	mongod --bind_ip_all --keyFile /data/configdb/mongo-keyfile --auth;

echo "starting Redis container"
podman pull docker.io/library/redis:8.6.4;
podman run \
	--network host -d --name redis docker.io/library/redis:8.6.4 \
	redis-server --requirepass "$REDIS_PASSWORD"

echo "starting Elasticsearch container"
podman pull docker.io/library/elasticsearch:9.4.3;
podman run \
  -e ELASTIC_PASSWORD="$ELASTIC_PASSWORD" \
  -v $SERVICES_ROOT/elasticsearch.yml:/usr/share/elasticsearch/config/elasticsearch.yml:Z \
  -v $SERVICES_ROOT/jvm-heap.options:/usr/share/elasticsearch/config/jvm.options.d/heap.options:Z \
  --network host --ulimit memlock=536870912:536870912 -d --name elasticsearch \
  docker.io/library/elasticsearch:9.4.3
