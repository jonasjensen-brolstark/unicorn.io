package io.unicorn;

import io.quarkus.test.junit.QuarkusTest;
import io.quarkus.test.junit.mockito.InjectMock;

import org.junit.jupiter.api.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.is;
import static org.mockito.Mockito.when;

@QuarkusTest
public class ProfileResourceTest {
    

    @InjectMock
    ProfileService profileService;

    // @Test
    // public void testProfileEndpoint() {
    //     when(profileService.getProfiles()).thenReturn(null);

    //     given()
    //       .when().get("/profile")
    //       .then()
    //          .statusCode(200)
    //          .body(is("Hello RESTEasy"));
    // }

}