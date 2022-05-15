
# description of script

# exit when any command fails
set -e

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
echo "[CMD]: docker build --no-cache -t $PROJECT:$VERSION -f ./$DOCKERFILE .."
docker build --no-cache -t $PROJECT:$VERSION -f ../$DOCKERFILE ..

# TAG
echo "[CMD]: docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:$VERSION"
docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:$VERSION
echo "[CMD]: docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:build"
docker tag $PROJECT:$VERSION $PROFILE/$PROJECT:build

# PUSH
echo "[CMD]: docker push $PROFILE/$PROJECT:$VERSION"
docker push $PROFILE/$PROJECT:$VERSION
docker push $PROFILE/$PROJECT:build

# finished
echo "[INFO]: FINISHED BUILD ($PROFILE/$PROJECT:$VERSION)!"