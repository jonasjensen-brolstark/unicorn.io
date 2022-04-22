# Snippets

## Creation

quarkus create app io.unicorn:profile

## API

### Json extension

quarkus extension add 'resteasy-jsonb'

### Model

```java
public class Profile {
    public String Id;
    public String Name;
    public int Age;
    public double Weight;

    public Profile() {
    }

    public Profile(String Id, String Name, int Age, double Weight) {
        this.Id = Id;
        this.Name = Name;
        this.Age = Age;
        this.Weight = Weight;
    }
}
```

### Profiles

```java
public final List<Profile> profiles = Arrays.asList(
    new Profile("2bfb422d-7b50-41e7-86df-ecfc1ebde843", "Rainbow", 19, 100),
    new Profile("0fd000a6-2cbf-4545-b7dd-099ebb170abf", "Happy", 20, 200),
    new Profile("586adccb-d597-4eb0-b992-f43280c8ff5d", "Spotty", 21, 300),
    new Profile("ed4ef265-d893-4fec-8ba9-743906c066e1", "John", 22, 400));
```

## APM

### opentelemetry extension

quarkus extension add 'opentelemetry-otlp-exporter'

### Config

quarkus.application.name=profile
quarkus.opentelemetry.tracer.exporter.otlp.endpoint=http://apm:8200
