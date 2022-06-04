
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
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:$VERSION-forArm
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:$VERSION-build-forArm
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:build-forArm
docker tag $PROJECT.cli:$VERSION $PROFILE/$PROJECT.cli:latest-forArm
# api
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:$VERSION-forArm
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:$VERSION-build-forArm
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:build-forArm
docker tag $PROJECT.api:$VERSION $PROFILE/$PROJECT.api:latest-forArm
# client
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:$VERSION-forArm
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:$VERSION-build-forArm
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:build-forArm
docker tag $PROJECT.client:$VERSION $PROFILE/$PROJECT.client:latest-forArm

# PUSH
# cli
docker push $PROFILE/$PROJECT.cli:$VERSION-forArm
docker push $PROFILE/$PROJECT.cli:$VERSION-build-forArm
docker push $PROFILE/$PROJECT.cli:build-forArm
docker push $PROFILE/$PROJECT.cli:latest-forArm
# api
docker push $PROFILE/$PROJECT.api:$VERSION
docker push $PROFILE/$PROJECT.api:latest
docker push $PROFILE/$PROJECT.api:$VERSION-forArm
docker push $PROFILE/$PROJECT.api:$VERSION-build-forArm
docker push $PROFILE/$PROJECT.api:build-forArm
docker push $PROFILE/$PROJECT.api:latest-forArm
# client
docker push $PROFILE/$PROJECT.client:$VERSION
docker push $PROFILE/$PROJECT.client:latest
docker push $PROFILE/$PROJECT.client:$VERSION-forArm
docker push $PROFILE/$PROJECT.client:$VERSION-build-forArm
docker push $PROFILE/$PROJECT.client:build-forArm
docker push $PROFILE/$PROJECT.client:latest-forArm

# docker build, tag, and push RELEASE script finished
