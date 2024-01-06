
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    //initialize with non-nullable value, avoid Nullable warnings
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    public ICollection<PointOfInterestDto>  PointsOfInterest {get; set;} = new List<PointOfInterestDto>();

    public City(string name)
    {
       Name = name;
    }

}