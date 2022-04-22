package io.unicorn.models;

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
