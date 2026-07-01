echo "access containers by names"
echo "127.0.0.1 mongo sql redis" >> /etc/hosts

echo "waiting for rootless podman to be ready..."
until podman info >/dev/null 2>&1; do
    sleep 2
done

echo "stop podman containers"
podman stop -t 0 -a >/dev/null 2>&1 || true

echo "start podman containers"
podman start mongo redis sql
