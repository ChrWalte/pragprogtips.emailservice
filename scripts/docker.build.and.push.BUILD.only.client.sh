
# description of script

# exit when any command fails
set -ev

# variables
# docker hub profile
PROFILE="chrwalte"
# project name
PROJECT="pragmatic.programmer.tips.client"
# version of the project
VERSION=$(cat ../VERSION)build
# the docker file to use
DOCKERFILE="Dockerfile.client"

# BUILD
docker build --no-cache -t $PROJECT:$VERSION -f ../$DOCKERFILE ..

# TAG
docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:$VERSION
docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:build

# PUSH
docker push $PROFILE/$PROJECT:$VERSION
docker push $PROFILE/$PROJECT:build

# docker build, tag, and push BUILD only client script finished
