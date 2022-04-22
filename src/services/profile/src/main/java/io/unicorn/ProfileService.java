package io.unicorn;

import java.util.Arrays;
import java.util.List;

import javax.enterprise.context.ApplicationScoped;

import io.unicorn.models.Profile;

@ApplicationScoped
public class ProfileService {
    private final List<Profile> profiles = Arrays.asList(
        new Profile("2bfb422d-7b50-41e7-86df-ecfc1ebde843", "Rainbow", 19, 100),
        new Profile("0fd000a6-2cbf-4545-b7dd-099ebb170abf", "Happy", 20, 200),
        new Profile("586adccb-d597-4eb0-b992-f43280c8ff5d", "Spotty", 21, 300),
        new Profile("ed4ef265-d893-4fec-8ba9-743906c066e1", "John", 22, 400));

    public List<Profile> getProfiles() {
        return profiles;
    }
}
