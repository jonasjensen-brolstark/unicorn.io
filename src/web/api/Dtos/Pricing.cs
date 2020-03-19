using System;

namespace api.Dtos {
  public class Pricing {
    public Guid Id { get; set; }
    public Guid UnicornId { get; set; } 
    public double Price { get; set; }
  }
}