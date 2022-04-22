call mvn clean package -DskipTests -Dmaven.javadoc.skip=true -Dmaven.source.skip=true
docker build -f src/main/docker/Dockerfile.jvm -t quarkus/profile-jvm .