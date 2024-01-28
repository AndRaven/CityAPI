public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetCitiesAsync();

    Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

    //make the return object nullable as the city could be missing
    Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest = false);

    Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

    Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);

    Task<bool> CityExistsAsync(int cityId);

    Task AddPointOfInterestToCityAsync(int cityId, PointOfInterest pointOfInterest);

    Task<bool> SaveChangesAsync();
}