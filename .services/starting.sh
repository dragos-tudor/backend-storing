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


echo "starting Kafka container"
podman pull docker.io/apache/kafka:4.3.1;
podman run \
  -e KAFKA_NODE_ID=1 \
  -e KAFKA_LISTENERS='CONTROLLER://kafka:9093,BROKER://kafka:9094,EXTERNAL://kafka:9092' \
  -e KAFKA_ADVERTISED_LISTENERS='BROKER://kafka:9094,EXTERNAL://kafka:9092' \
  -e KAFKA_INTER_BROKER_LISTENER_NAME='BROKER' \
  -e KAFKA_LISTENER_SECURITY_PROTOCOL_MAP='CONTROLLER:PLAINTEXT,BROKER:PLAINTEXT,EXTERNAL:SASL_PLAINTEXT' \
  -e KAFKA_SASL_ENABLED_MECHANISMS='SCRAM-SHA-512' \
  -e KAFKA_LISTENER_NAME_EXTERNAL_SASL_ENABLED_MECHANISMS='SCRAM-SHA-512' \
  -e "KAFKA_LISTENER_NAME_EXTERNAL_SCRAM-SHA-512_SASL_JAAS_CONFIG=org.apache.kafka.common.security.scram.ScramLoginModule required;" \
  -e KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 \
  -e KAFKA_PROCESS_ROLES='broker,controller' \
  -e KAFKA_CONTROLLER_QUORUM_VOTERS='1@kafka:9093' \
  -e KAFKA_CONTROLLER_LISTENER_NAMES='CONTROLLER' \
  -e CLUSTER_ID='MkU3OEVBNTcwNTJENDM2Qk' \
  --network host -d --name kafka \
  docker.io/apache/kafka:4.3.1

echo "Waiting for Kafka to be ready..."
until podman exec kafka /opt/kafka/bin/kafka-broker-api-versions.sh \
  --bootstrap-server kafka:9094 >/dev/null 2>&1; do
  echo " still starting ..."
  sleep 2
done

podman exec -it kafka /opt/kafka/bin/kafka-configs.sh \
  --bootstrap-server kafka:9094 \
  --alter \
  --add-config "SCRAM-SHA-512=[password=${KAFKA_PASSWORD}]" \
  --entity-type users \
  --entity-name "${KAFKA_USERNAME}"
