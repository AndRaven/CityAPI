/// <summary>
/// A DTO for a City without points of interest
/// </summary>
public class CityWithoutPointsOfInterestDto 
{
    /// <summary>
    /// id of the city
    /// </summary>
    public int Id { get; set; }

    //initialize with non-nullable value, avoid Nullable warnings
    /// <summary>
    /// name of the city
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// city description
    /// </summary>
    public string? Description { get; set; }

}