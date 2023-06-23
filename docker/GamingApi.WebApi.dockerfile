FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY . .

RUN pwd && ls -la
RUN ./scripts/buildAndPublish.sh

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /api

COPY --from=build /src/dist .

ENTRYPOINT [ "dotnet", "/api/Yld.GamingApi.WebApi.dll" ]
