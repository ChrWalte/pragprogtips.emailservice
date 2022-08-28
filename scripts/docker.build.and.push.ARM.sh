
# description of script

# exit when any command fails and logs stuff
set -xe

# variables
# docker hub profile
PROFILE="chrwalte"
# project name
PROJECT="pragmatic.programmer.tips"
# version of the project
VERSION=$(cat ../VERSION)

# BUILD
# service
docker build --no-cache -t $PROJECT.service:$VERSION -f ../Dockerfile.service ..
# api
docker build --no-cache -t $PROJECT.api:$VERSION -f ../Dockerfile.api ..
# client
docker build --no-cache -t $PROJECT.client:$VERSION -f ../Dockerfile.client ..

# TAG
# service
docker tag $PROJECT.service:$VERSION $PROFILE/$PROJECT.service:$VERSION-forArm
docker tag $PROJECT.service:$VERSION $PROFILE/$PROJECT.service:$VERSION-build-forArm
docker tag $PROJECT.service:$VERSION $PROFILE/$PROJECT.service:build-forArm
docker tag $PROJECT.service:$VERSION $PROFILE/$PROJECT.service:latest-forArm
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
# service
docker push $PROFILE/$PROJECT.service:$VERSION-forArm
docker push $PROFILE/$PROJECT.service:$VERSION-build-forArm
docker push $PROFILE/$PROJECT.service:build-forArm
docker push $PROFILE/$PROJECT.service:latest-forArm
# api
docker push $PROFILE/$PROJECT.api:$VERSION-forArm
docker push $PROFILE/$PROJECT.api:$VERSION-build-forArm
docker push $PROFILE/$PROJECT.api:build-forArm
docker push $PROFILE/$PROJECT.api:latest-forArm
# client
docker push $PROFILE/$PROJECT.client:$VERSION-forArm
docker push $PROFILE/$PROJECT.client:$VERSION-build-forArm
docker push $PROFILE/$PROJECT.client:build-forArm
docker push $PROFILE/$PROJECT.client:latest-forArm
