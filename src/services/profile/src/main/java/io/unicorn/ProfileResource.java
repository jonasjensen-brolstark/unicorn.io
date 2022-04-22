package io.unicorn;

import java.util.Arrays;
import java.util.List;

import javax.inject.Inject;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;

import io.unicorn.models.Profile;

@Path("/profile")
public class ProfileResource {

    @Inject
    ProfileService profileService;

    @GET
    public List<Profile> hello() {
        return profileService.getProfiles();
    }

    @Path("/{id}")
    @GET
    public Profile getProfile(@PathParam("id") String id) {
        var optionalProfile = profileService.getProfiles().stream().filter(profile -> profile.Id.equals(id)).findFirst();

        if (optionalProfile.isEmpty()) {
            throw new IllegalArgumentException("Profile not found with id: " + id);
        }

        return optionalProfile.get();
    }
}