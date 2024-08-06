### Docker extensions library
Implementing my own tailor-made [still naive] [docker extensions](./Docker.Extensions/) test container library over [docker dotnet](https://github.com/dotnet/Docker.DotNet/) library benefits:
- *full control* over docker daemon connection.
- *full-control* over docker containers management.
- ability to use *non-ephemeral containers*.

Using *ephemeral containers* [cold testing]:
- building sql server container > 60 seconds [having sql server image downloaded].
- starting sql server container ~ 15 seconds.
- running sql server tests 1 second.
- stopping and removing sql server container ~ 10 seconds.
[4th gen Intel, 5.6k hdd, ubuntu 24.04].

Using *non-ephemeral containers* after warmup [hot testing]:
- running sql server tests 1 second.
- test initializer should cleaned up all databases [when using hard coded ids].

### Test containers library
[Test containers](https://dotnet.testcontainers.org/) library was built to programatically use *ephemeral containers* for testing.

Using [test containers](https://dotnet.testcontainers.org/) library issues:
- connecting from *dev container* to docker daemon host [over *unix socket* or *tcp 2375*].
- disabling resource reaper [with reuse true or *ryuk.disabled* envvar true].
