using System;
using System.ComponentModel.DataAnnotations;

namespace pricing.Models {
  public class Pricing {

    public Guid Id { get; set; }
    public Guid UnicornId { get; set; } 
    public double Price { get; set; }
  }
}