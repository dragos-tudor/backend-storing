STORING_NETWORK="storing-network"
if ! (podman network ls | grep $STORING_NETWORK > /dev/null); then
	echo "create storing network"
	podman network create --driver=bridge $STORING_NETWORK
fi
