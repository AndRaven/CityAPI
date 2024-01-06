
using Microsoft.EntityFrameworkCore;

public class CityInfoContext : DbContext
{

    //the nulll! operator asserts that an expression is not null and throws an exception if it is, avoiding extra null checks
    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<PointsOfInterest> PointsOfInterest { get; set; } = null!;

    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
    {
        
    }
}