
using System.Data;
using Microsoft.EntityFrameworkCore;

public class CityInfoRepository : ICityInfoRepository
{
    private CityInfoContext _context;

    public CityInfoRepository(CityInfoContext context)
    {
       _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> CityExistsAsync(int cityId)
    {
       return await _context.Cities.AnyAsync(c => c.Id == cityId);
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
    }

    
    public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
    {
    //    if (string.IsNullOrEmpty(name) &&
    //        string.IsNullOrEmpty(searchQuery))
    //       return await _context.Cities.OrderBy(c => c.Name).ToListAsync();

        //using IQueryable to take advantage of deferred execution so that the searching and filtering is done at database level
        //not on the collecion in memory
        var collection = _context.Cities as IQueryable<City>;

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            collection = collection.Where(c => c.Name == name);
        }

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            collection = collection.Where(c => c.Name.Contains(searchQuery) ||
                                          (c.Description != null && c.Description.Contains(searchQuery)));
        }

        var totalItems = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItems, pageSize, pageNumber);
       
       //query is sent to the database only at the end when the ToListAsync () is called
        var collectionToReturn = await collection.OrderBy(c => c.Name)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        //return a tuple with the collection and pagination metadata
        return  (collectionToReturn, paginationMetadata);
    }
    

    public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest = false)
    {
        if (includePointsOfInterest)
        {
            return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
    }

    public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
    {
        return await _context.PointsOfInterest.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
    {
        return await _context.PointsOfInterest.Where(p => p.CityId == cityId).ToListAsync();
    }

    public async Task AddPointOfInterestToCityAsync(int cityId, PointOfInterest pointOfInterest)
    {
        var city = await GetCityAsync(cityId, false);

        if (city != null)
        {
            city.PointsOfInterest.Add(pointOfInterest);
         
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
       return (await _context.SaveChangesAsync() >= 0);
    }

}