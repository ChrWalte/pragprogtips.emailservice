
# description of script

# exit when any command fails and logs stuff
set -xe

# variables
# docker hub profile
PROFILE="chrwalte"
# project name
PROJECT="pragmatic.programmer.tips"
# version of the project
VERSION=$(cat ../VERSION)build

# BUILD
# service
docker build --no-cache -t $PROJECT.service:$VERSION -f ../Dockerfile.service ..
# api
docker build --no-cache -t $PROJECT.api:$VERSION -f ../Dockerfile.api ..
# client
docker build --no-cache -t $PROJECT.client:$VERSION -f ../Dockerfile.client ..

# TAG
# service
docker tag $PROJECT.service:$VERSION $PROFILE/$PROJECT.service:$VERSION
docker tag $PROJECT.service:$VERSION $PROFILE/$PROJECT.service:build
# api
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:$VERSION
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:build
# client
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:$VERSION
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:build

# PUSH
# service
docker push $PROFILE/$PROJECT.service:$VERSION
docker push $PROFILE/$PROJECT.service:build
# api
docker push $PROFILE/$PROJECT.api:$VERSION
docker push $PROFILE/$PROJECT.api:build
# client
docker push $PROFILE/$PROJECT.client:$VERSION
docker push $PROFILE/$PROJECT.client:build
