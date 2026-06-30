echo "access containers by names"
echo "127.0.0.1 mongo mongo.dns.podman" >> /etc/hosts
echo "127.0.0.1 sql sql.dns.podman" >> /etc/hosts
echo "127.0.0.1 redis redis.dns.podman" >> /etc/hosts

echo "start containers"
podman stop mongo redis sql
podman stop mongo redis sql
podman start mongo redis sql
