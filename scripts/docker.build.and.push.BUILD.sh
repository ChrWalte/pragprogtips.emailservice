
# description of script

# exit when any command fails
set -e

# variables
# docker hub profile
PROFILE="chrwalte"
# project name
PROJECT="pragmatic.programmer.tips"
# version of the project
VERSION=$(cat ../VERSION)build

# BUILD
# cli
echo "[CMD]: docker build --no-cache -t $PROJECT.cli:$VERSION -f ./Dockerfile.cli .."
docker build --no-cache -t $PROJECT.cli:$VERSION -f ../Dockerfile.cli ..
# api
echo "[CMD]: docker build --no-cache -t $PROJECT.api:$VERSION -f ./Dockerfile.api .."
docker build --no-cache -t $PROJECT.api:$VERSION -f ../Dockerfile.api ..
# client
echo "[CMD]: docker build --no-cache -t $PROJECT.client:$VERSION -f ./Dockerfile.client .."
docker build --no-cache -t $PROJECT.client:$VERSION -f ../Dockerfile.client ..

# TAG
# cli
echo "[CMD]: docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:$VERSION"
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:$VERSION
echo "[CMD]: docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:build"
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:build
# api
echo "[CMD]: docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:$VERSION"
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:$VERSION
echo "[CMD]: docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:build"
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:build
# client
echo "[CMD]: docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:$VERSION"
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:$VERSION
echo "[CMD]: docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:build"
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:build

# PUSH
# cli
echo "[CMD]: docker push $PROFILE/$PROJECT.cli:$VERSION"
docker push $PROFILE/$PROJECT.cli:$VERSION
docker push $PROFILE/$PROJECT.cli:build
# api
echo "[CMD]: docker push $PROFILE/$PROJECT.api:$VERSION"
docker push $PROFILE/$PROJECT.api:$VERSION
docker push $PROFILE/$PROJECT.api:build
# client
echo "[CMD]: docker push $PROFILE/$PROJECT.client:$VERSION"
docker push $PROFILE/$PROJECT.client:$VERSION
docker push $PROFILE/$PROJECT.client:build

# finished
echo "[INFO]: FINISHED BUILD ($PROFILE/$PROJECT:$VERSION)!"