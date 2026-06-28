
rm -rf /run/libpod/alive;
podman start storing-mongo storing-redis storing-sql
podman start storing-mongo storing-redis storing-sql