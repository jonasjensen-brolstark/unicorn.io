using System;

namespace api.Models {
  public class Unicorn {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }
  }
}