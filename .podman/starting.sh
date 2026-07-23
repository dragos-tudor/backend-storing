echo "access containers by names"
echo "127.0.0.1 mongo sql redis elasticsearch" >> /etc/hosts

echo "waiting for rootless podman to be ready..."
until podman info >/dev/null 2>&1; do
    sleep 2
done

echo "stop podman containers"
podman stop -a >/dev/null 2>&1 || true

echo "start podman containers"
podman start mongo sql redis elasticsearch
