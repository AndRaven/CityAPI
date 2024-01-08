
using Microsoft.EntityFrameworkCore;

public class CityInfoContext : DbContext
{

    //the nulll! operator asserts that an expression is not null and throws an exception if it is, avoiding extra null checks
    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new City ("Melbourne") { Id = 1 },
            new City ("Adelaide") { Id = 2 },
            new City ("Bucharest") { Id = 3 }
        );

        modelBuilder.Entity<PointOfInterest>().HasData(
            new PointOfInterest ("Spinning Wheel") { Id = 1, CityId = 1, Description = "A spinning wheel" },
            new PointOfInterest ("State library") { Id = 2,  CityId = 2, Description = "A library" },
            new PointOfInterest ("Bookstore") { Id = 3, CityId = 3, Description = "A bookstore" }
        );
    }
}