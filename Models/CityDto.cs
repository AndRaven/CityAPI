public class CityDto
{
    public int Id { get; set; }

    //initialize with non-nullable value, avoid Nullable warnings
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int NumberOfPointsOfInterest { get; set; }
}