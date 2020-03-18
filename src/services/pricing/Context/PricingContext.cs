using Microsoft.EntityFrameworkCore;
using pricing.Models;

namespace pricing.Context
{
  public class PricingContext : DbContext
  {
    public PricingContext(DbContextOptions<PricingContext> options) : base(options)
    {
    }

    public DbSet<Pricing> Pricings { get; set; }
  }
}