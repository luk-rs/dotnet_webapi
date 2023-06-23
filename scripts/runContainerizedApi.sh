#!/bin/bash

# it's supposed to run this script from the root folder where the .sln is located
# so a typical invocation would be ./scripts/runContainerizedApi.sh

docker run -p 5000:80 yld/gamingapi/api:latest
