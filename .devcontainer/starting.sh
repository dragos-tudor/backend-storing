echo "access containers by names"
echo "127.0.0.1 mongo mongo.dns.podman" >> /etc/hosts
echo "127.0.0.1 sql sql.dns.podman" >> /etc/hosts
echo "127.0.0.1 redis redis.dns.podman" >> /etc/hosts

echo "waiting for rootless podman to be ready..."
until podman info >/dev/null 2>&1; do
    sleep 2
done

podman stop -t 0 mongo >/dev/null 2>&1 || true
podman stop -t 0 redis >/dev/null 2>&1 || true
podman stop -t 0 sql >/dev/null 2>&1 || true

echo "start containers"
podman start mongo redis sql
