
# description of script

# exit when any command fails and logs stuff
set -xe

# variables
# docker hub profile
PROFILE="chrwalte"
# project name
PROJECT="pragmatic.programmer.tips.api"
# version of the project
VERSION=$(cat ../VERSION)build
# the docker file to use
DOCKERFILE="Dockerfile.api"

# BUILD
docker build --no-cache -t $PROJECT:$VERSION -f ../$DOCKERFILE ..

# TAG
docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:$VERSION
docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:build

# PUSH
docker push $PROFILE/$PROJECT:$VERSION
docker push $PROFILE/$PROJECT:build
