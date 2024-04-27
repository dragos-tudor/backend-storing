
### Docker outside of docker
Connecting to host docker server from **dev container** using *docker.sock* [not a solution].
- add *"mounts": [ "source=/var/run/docker.sock,target=/var/run/docker.sock,type=bind"]* to `.devcontainer.json`.
- running dev container as *non-root user*.
- create *docker group* and add *non-root user* to *docker group* [as dev feature or Dockerfile commands].
- connect docker client to *unix socket*.
- issues:
  - sending *host docker group id* as build arg is not working [using **initializeCommand** and envvar]. Getting *docker group id* from *docker.sock* [`stat -c "%g" /var/run/docker.sock`] not a solution because docker socket is available only on docker container after mounting.
  - creating *docker group* and adding *non-root user* to *docker group* using **onCreateCommand** not a solution because docker container run as *non-root user* without having rights to do these operations.

Connecting to host docker server from **dev container** over `tcp 2375` [solution](https://gist.githubusercontent.com/styblope/dc55e0ad2a9848f2cc3307d4819d819f/raw/9e76020d6b72d10351eb9583c606a09d68aba070/docker-api-port.md).
- add to `/etc/docker/daemon.json` and enable docker server/daemon listening on tcp 2375:
```
{
  "hosts": [
    "tcp://0.0.0.0:2375",
    "unix:///var/run/docker.sock"
  ]
}
```
- add to `/etc/systemd/system/docker.service.d/override.conf`:
```
  [Service]
  ExecStart=
  ExecStart=/usr/bin/dockerd
```
- restart systemd daemon and docker :
```shell
systemctl daemon-reload
systemctl restart docker.service
```

### Test containers library
[Test containers](https://dotnet.testcontainers.org/) library was built to programatically use **ephemeral containers** for testing instead of using *docker compose* so.

Using [test containers](https://dotnet.testcontainers.org/) library issues:
- connecting from **dev container** to docker server host [over *unix socket* or *tcp 2375*].
- disabling reasource reaper [with reuse true or *ryuk.disabled* envvar true].

### Docker extensions library
Implementing my own taylor-made [still naive] [docker extensions](./Docker.Extensions/) test container library over [docker dotnet](https://github.com/dotnet/Docker.DotNet/) library benefits:
- *full control* over docker server connection.
- *full-control* over docker containers management.
- ability to use **non-ephemeral containers**.

Using **ephemeral containers** [each time running tests - cold tests]:
  - building sql server container > 60 seconds [having sql server image downloaded].
  - starting sql server container ~ 15 seconds.
  - running sql server tests 1 second.
  - stopping and removing sql server container ~ 10 seconds.
[4th gen Intel, 5.6k hdd, ubuntu 24.04].

Using **non-ephemeral containers** after warmup [each time running test - hot tests]:
  - running sql server tests 1 second.
  - at startup all databases should be cleaned up [when using hard coded ids].