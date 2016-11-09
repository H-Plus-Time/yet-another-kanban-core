# Yet Another Kanban Board

A fairly standard REST API, providing CRUD endpoints for the accompanying React app, with one twist - it's in C# (with a lot of the .NET Core niceties).

NB: skeleton code generated as per the [generator-aspnet](https://github.com/OmniSharp/generator-aspnet) yeoman recipe.

# Requirements
Either:
 * docker daemon
or:
 * .NET Core >= 1.0.0

# Setup
```bash
  dotnet restore
```

# Usage
```bash
  dotnet run
```

Alternatively, build and run in an isolated docker container:
```bash
  docker build -t "yakc:dockerfile" .
  docker run yakc:dockerfile
```
