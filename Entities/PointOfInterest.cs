
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PointOfInterest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    public City? city {get; set;}

    //not required to specify foreign key but it is best practice
    [ForeignKey("CityId")]
    public int CityId {get; set;}

    public PointOfInterest(string name)
    {
        Name = name;
    }
}