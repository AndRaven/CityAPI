
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PointsOfInterest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public City? city {get; set;}

    //not required to specify foreign key but it is best practice
    [ForeignKey("CityId")]
    public int CityId {get; set;}

    public PointsOfInterest(string name)
    {
        Name = name;
    }
}