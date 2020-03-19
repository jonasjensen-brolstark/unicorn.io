using System;

namespace profile.Models {
  public class Profile {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
  }
}