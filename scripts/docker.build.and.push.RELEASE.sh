
# description of script

# exit when any command fails
set -ev

# variables
# docker hub profile
PROFILE="chrwalte"
# project name
PROJECT="pragmatic.programmer.tips"
# version of the project
VERSION=$(cat ../VERSION)

# BUILD
# cli
docker build --no-cache -t $PROJECT.cli:$VERSION -f ../Dockerfile.cli ..
# api
docker build --no-cache -t $PROJECT.api:$VERSION -f ../Dockerfile.api ..
# client
docker build --no-cache -t $PROJECT.client:$VERSION -f ../Dockerfile.client ..

# TAG
# cli
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:$VERSION
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:latest
# api
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:$VERSION
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:latest
# client
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:$VERSION
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:latest

# PUSH
# cli
docker push $PROFILE/$PROJECT.cli:$VERSION
docker push $PROFILE/$PROJECT.cli:latest
# api
docker push $PROFILE/$PROJECT.api:$VERSION
docker push $PROFILE/$PROJECT.api:latest
# client
docker push $PROFILE/$PROJECT.client:$VERSION
docker push $PROFILE/$PROJECT.client:latest

# docker build, tag, and push RELEASE script finished
