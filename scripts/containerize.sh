#!/bin/bash

# it's supposed to run this script from the root folder where the .sln is located
# so a typical invocation would be ./scripts/containerize.sh

docker build --progress plain -f docker/GamingApi.WebApi.dockerfile -t yld/gamingapi/api:latest .
