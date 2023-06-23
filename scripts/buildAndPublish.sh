#!/bin/bash

# it's supposed to run this script from the root folder where the .sln is located
# so a typical invocation would be ./scripts/buildAnaPublish.sh

SLN="GamingApi.sln"
DIST="dist/"
API="src/WebApi/GamingApi.WebApi.csproj"

[ -d $DIST ] && rm -r $DIST

dotnet clean

dotnet restore

dotnet build --no-restore -c Release $SLN

dotnet test -c Release $SLN

dotnet publish --no-build --no-restore -c Release -o $DIST
