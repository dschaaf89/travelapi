using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public TravelApiContext(DbContextOptions<TravelApiContext> options)
        : base(options)
    {
    }

    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Review>()
          .HasData(
              new Review { ReviewId = 1, ReviewDetails = "Eh", Country = "USA", City = "Seattle", Rating = 3, Destination = "Pike Place Market" },
              new Review { ReviewId = 2, ReviewDetails = "Crazy people", Country = "USA", City = "Portland", Rating = 3, Destination = "Rose Garden" },
              new Review { ReviewId = 6, ReviewDetails = "Crazy people", Country = "USA", City = "Portland", Rating = 2, Destination = "Rose Garden" },
              new Review { ReviewId = 3, ReviewDetails = "Fun", Country = "USA", City = "Maui", Rating = 5, Destination = "Sandy beaches" },
              new Review { ReviewId = 4, ReviewDetails = "Nice", Country = "South Africa", City = "Cape Town", Rating = 4, Destination = "soccer stadium" },
              new Review { ReviewId = 5, ReviewDetails = "Ugh", Country = "China", City = "Beijing", Rating = 1, Destination = "Forbidden City" }
          );
    }
  }
}