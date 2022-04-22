call mvn package -Pnative "-Dquarkus.native.container-build=true"
docker build -f src/main/docker/Dockerfile.native-micro -t quarkus/profile .